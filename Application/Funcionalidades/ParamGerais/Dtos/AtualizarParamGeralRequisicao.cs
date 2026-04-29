using Domain.Entidades;

namespace Application.Funcionalidades.ParamGerais.Dtos
{
    public class AtualizarParamGeralRequisicao
    {
        public bool NotificarTarefasAntesDoInicio { get; set; }
        public int QuantidadeDateTimeAntesDoInicio { get; set; }
        public EnumUnidadeTempo Unidade { get; set; }
        public bool ReforcarlembreteNoProprioDia { get; set; }
        public bool ReceberNotificacaoPorEmail { get; set; }
        public bool ReceberNotificacaoPorWhatsApp { get; set; }
        public bool ReceberResumoDiario { get; set; }
        public TimeOnly HorarioResumoDiario { get; set; }
        public bool ArquivamentoAutomaticoTarefasConcluidas { get; set; }
        public bool ArquivarTarefasConcluidas { get; set; }
        public int QuantidadeDiasParaArquivamento { get; set; }
        public EnumListagemPadraoDeTarefas ListagemPadraoDeTarefas { get; set; }
        public EnumTelaInicial TelaInicial { get; set; }
        public EnumPrimeiroDiaDaSemana PrimeiroDiaDaSemana { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}


