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
using Application.Dtos.LembreteDtos;
using Application.Interfaces.UseCases;
using Application.UseCase.Lembrete;
using Application.Emails;
using Application.Interfaces.Email;
using Infra.Emails;

Host.CreateDefaultBuilder(args)
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

        services.AddHostedService<WorkerDeNotificacoes>();

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
        services.AddSingleton<IMessageConsumer, RabbitMessageConsumer>();
        services.AddScoped<IMessageHandler<LembreteMensagemDto>, EnviarLembretePorEmailMessageHandler>();
        services.AddScoped<IEnviarLembretePorEmailUseCase, EnviarLembretePorEmailUseCase>();
        services.AddScoped<LembreteEmailCompose>();
        services.AddScoped<IEmail, Email>();

        services.AddScoped<IMessageDispatcher, MessageDispatcher>();
       ;


    })
    .Build()
    .Run();