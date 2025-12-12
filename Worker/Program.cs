using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Adicionando a injeção de dependências necessárias
        services.AddHostedService<WorkerDeNotificacoes>();
        services.AddSingleton<IConfiguration>(hostContext.Configuration);  // Para passar a configuração para o Worker
    })
    .Build()
    .Run();
