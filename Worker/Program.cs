using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Repositorys.LembreteRep;
using Application.Services.ServLembretes;
using Application.Utils.Transacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.ContextEFs;

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

        // 🔹 Serviço
        services.AddScoped<IServLembrete, ServLembrete>();
    })
    .Build()
    .Run();
