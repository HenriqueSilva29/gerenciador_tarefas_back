using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repository.ContextEFs
{
    public class ContextEFFactory : IDesignTimeDbContextFactory<ContextEF>
    {
        public ContextEF CreateDbContext(string[] args)
        {
            // Pega o diretório do projeto a partir do primeiro argumento, se existir
            var basePath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            Console.WriteLine($"Base path usado: {basePath}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ContextEF>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ContextEF(optionsBuilder.Options);
        }
    }
}
