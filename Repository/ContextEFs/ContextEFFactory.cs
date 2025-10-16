using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repository.ContextEFs
{
    public class ContextEFFactory : IDesignTimeDbContextFactory<ContextEF>
    {
        public ContextEF CreateDbContext(string[] args)
        {
            // Configura as opções para o DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ContextEF>();

            var projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\API"); 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)  // Caminho para o diretório API
                .AddJsonFile("appsettings.json") // Arquivo de configuração
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new ContextEF(optionsBuilder.Options);
        }
    }
}
