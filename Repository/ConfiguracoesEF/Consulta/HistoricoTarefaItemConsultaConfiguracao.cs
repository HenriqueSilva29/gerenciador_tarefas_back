using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.ModelosConsulta.Tarefas;

namespace Repository.ConfiguracoesEF.Consulta
{
    public class HistoricoTarefaItemConsultaConfiguracao : IEntityTypeConfiguration<HistoricoTarefaItemConsultaModelo>
    {
        public void Configure(EntityTypeBuilder<HistoricoTarefaItemConsultaModelo> builder)
        {
            builder.HasNoKey();
            builder.ToView(null);

            builder.Property(x => x.IdAuditoria).HasColumnName("idauditoria");
            builder.Property(x => x.IdTarefaPrincipal).HasColumnName("id_tarefa_principal");
            builder.Property(x => x.IdTarefaRelacionada).HasColumnName("id_tarefa_relacionada");
            builder.Property(x => x.EscopoEvento).HasColumnName("escopo_evento");
            builder.Property(x => x.TipoEvento).HasColumnName("tipo_evento");
            builder.Property(x => x.Acao).HasColumnName("acao");
            builder.Property(x => x.IdUsuario).HasColumnName("idusuario");
            builder.Property(x => x.DataOcorrencia).HasColumnName("dataocorrencia");
            builder.Property(x => x.Alteracoes).HasColumnName("alteracoes");
            builder.Property(x => x.TituloRelacionado).HasColumnName("titulo_relacionado");
        }
    }
}

