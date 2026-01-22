using Domain.Common.ValueObjects;
using Domain.Entities.Lembretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF.LembreteConfigs
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

            builder.Property(l => l.PrazoDeAvisoAntesDoVencimento)
                .IsRequired(true)
                .HasColumnName("antecedencia");

            builder.Property(l => l.Texto)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnName("texto");

            builder.Property(l => l.Status)
                .IsRequired(true)
                .HasColumnName("status");

            builder.Property(l => l.DataDeAgendamento)
                .IsRequired(true)
                .HasColumnName("data_de_agendamento")
                .HasConversion(
                    utc => utc.Value,
                    value => UtcDateTime.From(value)
                  );

            builder.Property(l => l.DataDeExecucaoDoAgendamento)
                .IsRequired(false)
                .HasColumnName("data_de_execucao_do_agendamento")
                .HasConversion(
                    utc => utc.HasValue ? utc.Value.Value : (DateTimeOffset?)null,
                    value => value.HasValue ? UtcDateTime.From(value.Value) : null
                );
        }
    }
}
