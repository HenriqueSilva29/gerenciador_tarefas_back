using Domain.Common;

namespace Domain.Entities
{
    public class ParamGeral : IEntityId<int>
    {
        public int Id { get; set; }
        public bool NotificarTarefasAntesDoInicio { get; set; } = false;
        public int QuantidadeDateTimeAntesDoInicio { get; set; } = 1;
        public UnidadeTempo Unidade { get; set; } = UnidadeTempo.Dias;
        public bool ReforcarlembreteNoProprioDia { get; set; } = false;
        public bool ReceberNotificacaoPorEmail { get; set; } = false;
        public bool ReceberNotificacaoPorWhatsApp { get; set; } = false;
        public bool ReceberResumoDiario { get; set; } = false;
        public TimeOnly HorarioResumoDiario { get; set; }
        public bool ArquivamentoAutomaticoTarefasConcluidas { get; set; } = false;
        public bool ArquivarTarefasConcluidas { get; set; } = false;
        public int QuantidadeDiasParaArquivamento { get; set; } = 1;
        public EnumListagemPadraoDeTarefas ListagemPadraoDeTarefas { get; set; } = 0;
        public EnumTelaInicial TelaInicial { get; set; } = 0;
        public EnumPrimeiroDiaDaSemana PrimeiroDiaDaSemana { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }

    public enum EnumTelaInicial
    {
        ResumoGeral = 0,
        Tarefas = 1,
        Calendario = 2,
    } 

    public enum EnumListagemPadraoDeTarefas
    {
        lista = 0,
        agenda = 1
    }

    public enum EnumPrimeiroDiaDaSemana
    {
        Domingo = 0,
        Segunda = 1
    }

    public enum UnidadeTempo
    {
        Minutos,
        Horas,
        Dias
    }
}
