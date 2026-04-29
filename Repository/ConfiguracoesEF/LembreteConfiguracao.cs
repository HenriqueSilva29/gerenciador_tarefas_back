using Domain.Comum.ObjetosDeValor;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfiguracoesEF
{
    public class LembreteConfiguracao : IEntityTypeConfiguration<Lembrete>
    {
        public void Configure(EntityTypeBuilder<Lembrete> builder)
        {
            builder.ToTable("Lembrete");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("idLembrete")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.CodigoTarefa)
                .IsRequired()
                .HasColumnName("idtarefa");

            builder.HasOne(l => l.Tarefa)
                   .WithMany(t => t.Lembretes)
                   .HasForeignKey(l => l.CodigoTarefa)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.DataDisparo)
                .IsRequired()
                .HasColumnName("DataDisparo");

            builder.Property(l => l.Descricao)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnName("descricao");

            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("status");
        }
    }
}

