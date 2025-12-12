using Domain.Lembretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF.LembreteConfigs
{
    public class LembreteConfig : IEntityTypeConfiguration<Lembrete>
    {
        public void Configure(EntityTypeBuilder<Lembrete> builder)
        {
            builder.ToTable("Lembrete");

            builder.HasKey(l => l.CodigoLembrete);

            builder.Property(l => l.CodigoLembrete)
                .HasColumnName("idLembrete")
                .ValueGeneratedOnAdd();

            builder.HasOne(l => l.ToDoItem)
                   .WithMany(t => t.Lembretes)
                   .HasForeignKey(l => l.CodigoToDoItem)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.DataDoLembrete)
                .IsRequired();

            builder.Property(l => l.Texto)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(l => l.FoiEnviado)
                .IsRequired()
                .HasColumnName("foiEnviado");

            builder.Property(l => l.DataDeEnvio)
                .IsRequired(false)
                .HasColumnName("dataEnvio");
        }
    }
}
