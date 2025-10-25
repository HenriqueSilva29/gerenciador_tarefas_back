using Microsoft.EntityFrameworkCore;
using Repository.ConfigEF.ToDoItemConfig;
using Domain.ToDoItems;

namespace Repository.ContextEFs
{
    public class ContextEF : DbContext
    {
        public ContextEF() { }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ContextEF(DbContextOptions<ContextEF> options) : base(options) { }

        // Configuração das entidades usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica as configurações de todas as entidades
            modelBuilder.ApplyConfiguration(new ToDoItemConfig());
        }
    }
}
