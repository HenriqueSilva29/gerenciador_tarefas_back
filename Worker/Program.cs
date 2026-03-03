using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Repositorys.LembreteRep;
using Application.Utils.Transacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.ContextEFs;
using Infra.Mensageria.RabbitMQ.Consumidores;
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

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {
        // 🔥 MOSTRA O AMBIENTE LOGO NA INICIALIZAÇÃO
        Console.WriteLine("======================================");
        Console.WriteLine($"AMBIENTE ATUAL: {hostContext.HostingEnvironment.EnvironmentName}");
        Console.WriteLine("======================================");

        services.AddHostedService<Worker>();

        services.AddDbContext<ContextEF>(options =>
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Repository")));

        // 🔹 Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // 🔹 Repositório
        services.AddScoped<IRepLembrete, RepLembrete>();

        // 🔹 RabbitMQ
        services.AddSingleton<IRabbitConnection, RabbitConnection>();
        services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();
        services.AddSingleton<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
        services.AddSingleton<IMessageConsumer, NotificarEmailConsumer>();

        services.AddScoped<IMessageHandler<LembreteVencimentoAtingidoEvent>, EnviarLembretePorEmailMessageHandler>();
        services.AddScoped<IEnviarLembretePorEmailUseCase, EnviarLembretePorEmailUseCase>();
        services.AddScoped<LembreteEmailCompose>();
        services.AddScoped<IEmail, Email>();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

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

// 🔥 AQUI é o lugar certo
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