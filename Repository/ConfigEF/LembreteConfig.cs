using Domain.Common.ValueObjects;
using Domain.Entities.Lembretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF
{
    public class LembreteConfig : IEntityTypeConfiguration<Lembrete>
    {
        public void Configure(EntityTypeBuilder<Lembrete> builder)
        {
            builder.ToTable("Lembrete");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("idLembrete")
                .ValueGeneratedOnAdd();

            builder.HasOne(l => l.ToDoItem)
                   .WithMany(t => t.Lembretes)
                   .HasForeignKey(l => l.CodigoToDoItem)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.Texto)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnName("texto");

            builder.Property(l => l.Status)
                .IsRequired(true)
                .HasColumnName("status");
        }
    }
}
