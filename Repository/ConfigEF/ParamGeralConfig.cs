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

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                 .HasColumnName("idparamgeral")
                 .ValueGeneratedOnAdd();

            builder.Property(p => p.NotificarTarefasAntesDoInicio)
                 .HasColumnName("notificar_tarefas_antes_do_inicio")
                 .HasDefaultValue(false);

            builder.Property(p => p.QuantidadeDateTimeAntesDoInicio)
                 .HasColumnName("quantidade_datetime_antes_do_inicio")
                 .HasDefaultValue(1);

            builder.Property(p => p.Unidade)
                 .HasColumnName("unidade_antes_do_inicio")
                 .HasConversion<int>()
                 .HasDefaultValue(UnidadeTempo.Dias);

            builder.Property(p => p.ReforcarlembreteNoProprioDia)
                 .HasColumnName("reforcar_lembrete_no_proprio_dia")
                 .HasDefaultValue(false);

            builder.Property(p => p.ReceberNotificacaoPorEmail)
                 .HasColumnName("receber_notificacao_por_email")
                 .HasDefaultValue(false);

            builder.Property(p => p.ReceberNotificacaoPorWhatsApp)
                 .HasColumnName("receber_notificacao_por_whatsapp")
                 .HasDefaultValue(false);

            builder.Property(p => p.ReceberResumoDiario)
                 .HasColumnName("receber_resumo_diario")
                 .HasDefaultValue(false);

            builder.Property(p => p.HorarioResumoDiario)
                 .HasColumnName("horario_resumo_diario")
                 .HasConversion(
                    horario => horario.ToTimeSpan(),
                    horario => TimeOnly.FromTimeSpan(horario));

            builder.Property(p => p.ArquivamentoAutomaticoTarefasConcluidas)
                 .HasColumnName("arquivamento_automatico_tarefas_concluidas")
                 .HasDefaultValue(false);

            builder.Property(p => p.ArquivarTarefasConcluidas)
                 .HasColumnName("arquivar_tarefas_concluidas")
                 .HasDefaultValue(false);

            builder.Property(p => p.QuantidadeDiasParaArquivamento)
                 .HasColumnName("quantidade_dias_para_arquivamento")
                 .HasDefaultValue(1);

            builder.Property(p => p.ListagemPadraoDeTarefas)
                 .HasColumnName("listagem_padrao_de_tarefas")
                 .HasConversion<int>()
                 .HasDefaultValue(EnumListagemPadraoDeTarefas.lista);

            builder.Property(p => p.TelaInicial)
                 .HasColumnName("tela_inicial")
                 .HasConversion<int>()
                 .HasDefaultValue(EnumTelaInicial.ResumoGeral);

            builder.Property(p => p.PrimeiroDiaDaSemana)
                 .HasColumnName("primeiro_dia_da_semana")
                 .HasConversion<int>()
                 .HasDefaultValue(EnumPrimeiroDiaDaSemana.Domingo);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(200);

            builder.Property(p => p.Telefone)
                .HasColumnName("telefone")
                .HasMaxLength(200);
        }
    }
}
