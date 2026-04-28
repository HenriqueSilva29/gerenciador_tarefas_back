using Domain.Common.ValueObjects;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Repository.ConfigEF
{
    public class NotificacaoConfig : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("Notificacao");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasColumnName("idnotificacao")
                .ValueGeneratedOnAdd();

            builder.Property(n => n.CodigoUsuario)
                .HasColumnName("idusuario");

            builder.HasOne(n => n.Usuario)
                .WithMany()
                .HasForeignKey(n => n.CodigoUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.Tipo)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("tipo");

            builder.Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("titulo");

            builder.Property(n => n.Mensagem)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("mensagem");

            builder.Property(n => n.Lida)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("lida");

            builder.Property(n => n.DataCriacao)
                .IsRequired()
                .HasColumnName("data_criacao")
                .HasConversion(
                    utc => utc.Value,
                    utc => UtcDateTime.From(utc));

            builder.Property(n => n.DataLeitura)
                .HasColumnName("data_leitura")
                .HasConversion(
                    utc => utc.HasValue ? utc.Value.Value : (DateTimeOffset?)null,
                    utc => utc.HasValue ? UtcDateTime.From(utc.Value) : null);
        }
    }
}
