using Domain.ToDoItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF.ToDoItemConfigs
{
    public class ToDoItemConfig : IEntityTypeConfiguration<ToDoItem>
    {
        //modelo Fluent API
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {

            builder.ToTable("ToDoItem");

            builder.HasKey(t => t.CodigoToDoItem);

            builder.HasMany(t => t.SubTarefas)
                   .WithOne(t => t.ToDoItemPai)
                   .HasForeignKey(t => t.CodigoToDoItemPai)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.CodigoToDoItem)
                 .HasColumnName("idToDoItem")
                 .ValueGeneratedOnAdd();

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Descricao)
                .HasMaxLength(500);

            builder.Property(t => t.Prioridade)
                .IsRequired()
                .HasConversion<int>(); 

            builder.Property(t => t.Categoria)
                .HasConversion<int>();  

            builder.Property(t => t.Status)
                .HasConversion<int>();  

            builder.Property(t => t.DataCriacao)
                .IsRequired();

            builder.Property(t => t.DataVencimento)
                .IsRequired(false);
        }
    }
}
