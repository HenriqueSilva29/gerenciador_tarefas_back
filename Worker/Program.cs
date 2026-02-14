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

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<WorkerDeNotificacoes>();

        services.AddDbContext<ContextEF>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                                 x => x.MigrationsAssembly("Repository")));

        // 🔹 Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // 🔹 Repositório
        services.AddScoped<IRepLembrete, RepLembrete>();

        services.AddSingleton<IRabbitConnection, RabbitConnection>();
        services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();
        services.AddSingleton<IMessageConsumer, RabbitMessageConsumer>();
        services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
        services.AddHostedService<WorkerDeNotificacoes>();
    })
    .Build()
    .Run();
