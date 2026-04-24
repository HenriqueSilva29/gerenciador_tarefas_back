using Application.Dtos.ParamGerals;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ParamGeralMapper
    {
        public static ParamGeral AtualizarEntidade(ParamGeral entity, UpdateParamGeralRequest dto)
        {
            entity.NotificarTarefasAntesDoInicio = dto.NotificarTarefasAntesDoInicio;
            entity.QuantidadeDateTimeAntesDoInicio = dto.QuantidadeDateTimeAntesDoInicio;
            entity.Unidade = dto.Unidade;
            entity.ReforcarlembreteNoProprioDia = dto.ReforcarlembreteNoProprioDia;
            entity.ReceberNotificacaoPorEmail = dto.ReceberNotificacaoPorEmail;
            entity.ReceberNotificacaoPorWhatsApp = dto.ReceberNotificacaoPorWhatsApp;
            entity.ReceberResumoDiario = dto.ReceberResumoDiario;
            entity.HorarioResumoDiario = dto.HorarioResumoDiario;
            entity.ArquivamentoAutomaticoTarefasConcluidas = dto.ArquivamentoAutomaticoTarefasConcluidas;
            entity.ArquivarTarefasConcluidas = dto.ArquivarTarefasConcluidas;
            entity.QuantidadeDiasParaArquivamento = dto.QuantidadeDiasParaArquivamento;
            entity.ListagemPadraoDeTarefas = dto.ListagemPadraoDeTarefas;
            entity.TelaInicial = dto.TelaInicial;
            entity.PrimeiroDiaDaSemana = dto.PrimeiroDiaDaSemana;
            entity.Email = dto.Email;
            entity.Telefone = dto.Telefone;

            return entity;
        }
    }
}
