using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ConfigEF
{
    public class ParamGeralConfig : IEntityTypeConfiguration<ParamGeral>
    {
        public void Configure(EntityTypeBuilder<ParamGeral> builder)
        {

            builder.ToTable("ParamGeral");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                 .HasColumnName("idparamgeral")
                 .ValueGeneratedOnAdd();

            builder.Property(t => t.AvisarVencimento)
                 .HasColumnName("avisar_vencimento")
                 .HasDefaultValue(false);

            builder.Property(t => t.DiasAntesDoVencimento)
                 .HasColumnName("dias_antes_do_vencimento")
                 .HasDefaultValue(0);

        }
    }
}