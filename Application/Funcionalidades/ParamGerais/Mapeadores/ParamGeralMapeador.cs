using Application.Funcionalidades.ParamGerais.Dtos;
using Domain.Entidades;

namespace Application.Funcionalidades.ParamGerais.Mapeadores
{
    public static class ParamGeralMapeador
    {
        public static ParamGeral AtualizarEntidade(ParamGeral entity, AtualizarParamGeralRequisicao dto)
        {
            entity.Atualizar(
                dto.NotificarTarefasAntesDoInicio,
                dto.QuantidadeDateTimeAntesDoInicio,
                dto.Unidade,
                dto.ReforcarlembreteNoProprioDia,
                dto.ReceberNotificacaoPorEmail,
                dto.ReceberNotificacaoPorWhatsApp,
                dto.ReceberResumoDiario,
                dto.HorarioResumoDiario,
                dto.ArquivamentoAutomaticoTarefasConcluidas,
                dto.ArquivarTarefasConcluidas,
                dto.QuantidadeDiasParaArquivamento,
                dto.ListagemPadraoDeTarefas,
                dto.TelaInicial,
                dto.PrimeiroDiaDaSemana,
                dto.Email,
                dto.Telefone);

            return entity;
        }
    }
}


