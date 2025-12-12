using Microsoft.EntityFrameworkCore;
using Repository.ConfigEF.ToDoItemConfigs;
using Domain.ToDoItems;
using Repository.ConfigEF.LembreteConfigs;

namespace Repository.ContextEFs
{
    public class ContextEF : DbContext
    {
        public ContextEF(DbContextOptions<ContextEF> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ToDoItemConfig());
            modelBuilder.ApplyConfiguration(new LembreteConfig());
        }
    }
}
