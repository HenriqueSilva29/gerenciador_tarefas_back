using Domain.Common.ValueObjects;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF
{
    public class TarefaConfig : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {

            builder.ToTable("Tarefa");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.SubTarefas)
                   .WithOne(t => t.TarefaPai)
                   .HasForeignKey(t => t.CodigoTarefaPai)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.Id)
                 .HasColumnName("idtarefa")
                 .ValueGeneratedOnAdd();

            builder.Property(t => t.CodigoTarefaPai)
                 .HasColumnName("idtarefapai");

            builder.Property(t => t.CodigoUsuario)
                 .HasColumnName("idusuario");

            builder.HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.CodigoUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("titulo");

            builder.Property(t => t.Descricao)
                .HasMaxLength(500)
                .HasColumnName("descricao");

            builder.Property(t => t.Prioridade)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("prioridade");

            builder.Property(t => t.Categoria)
                .HasConversion<int>()
                .HasColumnName("categoria");

            builder.Property(t => t.Status)
                .HasConversion<int>()
                .HasColumnName("status");

            builder.Property(t => t.DataCriacao)
                .IsRequired()
                .HasColumnName("data_criacao")
                .HasConversion(
                    utc => utc.Value,
                    utc => UtcDateTime.From(utc)
                );

            builder.Property(t => t.DataVencimento)
                .IsRequired(true)
                .HasColumnName("data_vencimento")
                .HasConversion(
                    utc => utc.Value,
                    utc => UtcDateTime.From(utc)
                );

            builder.Property(t => t.DataTarefa)
                .IsRequired()
                .HasColumnName("data_tarefa")
                .HasConversion(
                    data => data.ToDateTime(TimeOnly.MinValue),
                    data => DateOnly.FromDateTime(data)
                );

            builder.Property(t => t.HoraInicio)
                .IsRequired()
                .HasColumnName("hora_inicio")
                .HasConversion(
                    hora => hora.ToTimeSpan(),
                    hora => TimeOnly.FromTimeSpan(hora)
                );

            builder.Property(t => t.HoraFim)
                .IsRequired()
                .HasColumnName("hora_fim")
                .HasConversion(
                    hora => hora.ToTimeSpan(),
                    hora => TimeOnly.FromTimeSpan(hora)
                );
        }
    }
}
