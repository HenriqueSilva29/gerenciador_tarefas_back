using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF.ToDoItemConfig
{
    public class ToDoItemConfig : IEntityTypeConfiguration<ToDoItem>
    {
        //modelo Fluent API
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {

            builder.ToTable("ToDoItem");

            builder.HasKey(t => t.idToDoItem);

            builder.Property(t => t.idToDoItem)
                 .ValueGeneratedOnAdd();

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Descricao)
                .HasMaxLength(500);

            builder.Property(t => t.Prioridade)
                .IsRequired();

            builder.Property(t => t.Categoria)
                .HasMaxLength(100);

            builder.Property(t => t.DataCriacao)
                .IsRequired();

            builder.Property(t => t.DataVencimento)
                .IsRequired(false);

            builder.Property(t => t.Concluido)
                .HasDefaultValue(false);
        }
    }
}
