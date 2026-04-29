using Domain.Comum.ObjetosDeValor;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfiguracoesEF
{
    public class AuditoriaConfiguracao : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder) 
        {
            builder.ToTable("auditoria");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("idauditoria")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Entidade)
                .HasColumnName("entidade")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.IdEntidade)
                .HasColumnName("identidade")
                .IsRequired();

            builder.Property(a => a.Acao)
                .HasColumnName("acao")
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(a => a.IdUsuario)
                .HasColumnName("idusuario")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Data)
                .HasColumnName("dataocorrencia")
                .HasConversion(
                    utc => utc.Value,
                    utc => UtcDateTime.From(utc))
                .IsRequired();

            builder.Property(a => a.Alteracoes)
                .HasColumnName("alteracoes")
                .HasColumnType("nvarchar(max)") // ou "text" se for PostgreSQL
                .IsRequired();

            // Índices importantes para performance
            builder.HasIndex(a => a.IdEntidade)
                .HasDatabaseName("ix_auditoria_identidade");

            builder.HasIndex(a => a.Data)
                .HasDatabaseName("ix_auditoria_data");

            builder.HasIndex(a => a.IdUsuario)
                .HasDatabaseName("ix_auditoria_usuario");
        }
    }
}

