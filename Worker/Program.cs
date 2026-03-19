using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Repositorys.LembreteRep;
using Application.Utils.Transacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.ContextEFs;
using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Mensageria.RabbitMQ.Connections;
using Infra.Mensageria.RabbitMQ.Topology;
using Application.Emails;
using Application.Interfaces.Email;
using Infra.Emails;
using Application.UseCase.Lembretes;
using Hangfire;
using Hangfire.SqlServer;
using Infra.BackgroundJobs.Hangfire.Jobs.Lembretes;
using Application.Interfaces.UseCases.Lembretes;
using Infra.Messaging.RabbitMQ.Topology;
using Infra.Messaging.RabbitMQ.Consumidores.Lembretes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Lembretes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Tarefas;
using Application.Events.Tarefas;
using Application.Messaging.MessageHandlers;
using Infra.Messaging.RabbitMQ.Consumidores.Tarefas;
using Repository.TarefaRep;
using Repository.Repositorys.ParamGeralRep;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {
        Console.WriteLine("======================================");
        Console.WriteLine($"Iniciando Worker...");
        Console.WriteLine("======================================");

        services.AddHostedService<RabbitInitializerHostedService>();
        services.AddHostedService<Worker>();

        services.AddDbContext<ContextEF>(options =>
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Repository")));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositório
        services.AddScoped<IRepLembrete, RepLembrete>();
        services.AddScoped<IRepTarefa, RepTarefa>();
        services.AddScoped<IRepParamGeral, RepParamGeral>();

        // RabbitMQ
        services.AddSingleton<IRabbitConnection, RabbitConnection>();
        services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();

        //Consumidores
        services.AddSingleton<IMessageConsumer, EnviarLembreteVencidoPorEmailConsumer>();
        services.AddSingleton<IMessageConsumer, TarefaCriadaGerarLembreteConsumer>();

        //Evento
        services.AddScoped<IMessageHandler<LembreteVencimentoAtingidoEvent>, EnviarLembretePorEmailMessageHandler>();
        services.AddScoped<IMessageHandler<TarefaCriadaEvent>, TarefaCriadaGerarLembreteMessageHandler>();

        //UseCase
        services.AddScoped<IEnviarLembretePorEmailUseCase, EnviarLembretePorEmailUseCase>();
        services.AddScoped<ITarefaCriadaGerarLembreteUseCase,TarefaCriadaGerarLembreteUseCase>();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        //Topologia
        services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
        services.AddScoped<IRabbitTopology, NotificacaoEmailLembreteVencimentoTopology>();
        services.AddScoped<IRabbitTopology, TarefaCriadaGerarLembreteTopology>();

        services.AddScoped<LembreteEmailCompose>();
        services.AddScoped<IEmail, Email>();

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromSeconds(60)
                });
        });
        services.AddHangfireServer(options =>
        {
            options.ServerName = "API-Hangfire-Server";
        });

    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider
        .GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate<VerificarLembretesVencendoJob>(
        "verificar-lembretes",
        job => job.Execute(),
        "*/5 * * * *"
    );
}

host.Run();