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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ToDoItemConfig());
        }
    }
}
