using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Repositorios.Lembretes;
using Application.Utils.Transacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.ContextosEF;
using Application.Interfaces.Messaging;
using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ.Connections;
using Infra.Messaging.RabbitMQ.Publicadores;
using Infra.Messaging.RabbitMQ.Topology;
using Application.Emails;
using Application.Interfaces.Email;
using Application.Funcionalidades.Lembretes.CasosDeUso;
using Hangfire;
using Hangfire.SqlServer;
using Infra.BackgroundJobs.Hangfire.Jobs.Lembretes;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Infra.Messaging.RabbitMQ.Consumidores;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Tarefas;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Notificacoes;
using Application.Funcionalidades.Tarefas.Eventos;
using Application.Messaging.MessageHandlers;
using Infra.Messaging.RabbitMQ.Consumidores.Tarefas;
using Repository.Repositorios.Tarefas;
using Repository.Repositorios.Notificacoes;
using Repository.Repositorios.ParamGerais;
using Infra.Notificacoes;
using System.Text;
using Application.Funcionalidades.Notificacoes.CasosDeUso;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
        config.AddJsonFile(
            $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.Local.json",
            optional: true,
            reloadOnChange: true);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {
        ValidateRequiredConfiguration(hostContext.Configuration);

        Console.WriteLine("======================================");
        Console.WriteLine($"Iniciando Worker...");
        Console.WriteLine("======================================");

        services.AddHostedService<RabbitInitializerHostedService>();
        services.AddHostedService<RabbitConsumerHostedService>();

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
        services.AddScoped<IRepNotificacao, RepNotificacao>();

        // RabbitMQ
        services.AddSingleton<IRabbitConnection, RabbitConnection>();
        services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();
        services.AddScoped<IRabbitEventPublisher, RabbitEventPublisher>();

        //Consumidores
        services.AddSingleton<IMessageConsumer, GerarLembreteConsumer>();

        //Evento
        services.AddScoped<IMessageHandler<TarefaCriadaEvento>, GerarLembreteMessageHandler>();

        //UseCase
        services.AddScoped<IEnviarLembretePorEmailCasoDeUso, EnviarLembretePorEmailCasoDeUso>();
        services.AddScoped<IGerarLembreteCasoDeUso,TarefaCriadaGerarLembreteCasoDeUso>();
        services.AddScoped<IAgendadorJobLembrete, AgendarLembreteJobScheduler>();
        services.AddScoped<IAgendarLembreteCasoDeUso, AgendarLembreteCasoDeUso>();
        services.AddScoped<IDispararLembreteCasoDeUso, DispararLembreteCasoDeUso>();
        services.AddScoped<ICriarNotificacaoCasoDeUso, CriarNotificacaoCasoDeUso>();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        //Topologia
        services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
        services.AddScoped<IRabbitTopology, GerarLembreteTopology>();
        services.AddScoped<IRabbitTopology, NotificacaoCriadaTopology>();

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
            options.ServerName = "Worker-Hangfire-Server";
        });

        services.Configure<OpcoesEmail>(
            hostContext.Configuration.GetSection("Email"));

    })
    .Build();

host.Run();

static void ValidateRequiredConfiguration(IConfiguration configuration)
{
    var missingKeys = new List<string>();

    Require("ConnectionStrings:DefaultConnection");
    Require("RabbitMQ:Uri");
    Require("Email:Host");
    Require("Email:Port");
    Require("Email:UserName");
    Require("Email:Password");
    Require("Email:FromEmail");
    Require("Email:FromName");

    if (missingKeys.Count > 0)
    {
        var message = new StringBuilder()
            .AppendLine("Configuracao obrigatoria ausente para iniciar o Worker.")
            .AppendLine("Revise os arquivos appsettings.Local.json / appsettings.{Environment}.Local.json.")
            .AppendLine("Chaves ausentes:");

        foreach (var key in missingKeys)
        {
            message.AppendLine($"- {key}");
        }

        throw new InvalidOperationException(message.ToString());
    }

    void Require(string key)
    {
        if (string.IsNullOrWhiteSpace(configuration[key]))
        {
            missingKeys.Add(key);
        }
    }
}


