using Domain.Comum;

namespace Domain.Entidades
{
    public class ParamGeral : IEntidadeId<int>
    {
        public int Id { get; set; }
        public int CodigoUsuario { get; set; }
        public Usuario Usuario { get; set; } = default!;
        public bool NotificarTarefasAntesDoInicio { get; set; } = false;
        public int QuantidadeDateTimeAntesDoInicio { get; set; } = 1;
        public EnumUnidadeTempo Unidade { get; set; } = EnumUnidadeTempo.Minutos;
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

        public static ParamGeral CriarPadrao(Usuario usuario)
        {
            return new ParamGeral
            {
                Usuario = usuario,
                Email = usuario.Email,
                HorarioResumoDiario = new TimeOnly(8, 0)
            };
        }

        public void Atualizar(
            bool notificarTarefasAntesDoInicio,
            int quantidadeDateTimeAntesDoInicio,
            EnumUnidadeTempo unidade,
            bool reforcarlembreteNoProprioDia,
            bool receberNotificacaoPorEmail,
            bool receberNotificacaoPorWhatsApp,
            bool receberResumoDiario,
            TimeOnly horarioResumoDiario,
            bool arquivamentoAutomaticoTarefasConcluidas,
            bool arquivarTarefasConcluidas,
            int quantidadeDiasParaArquivamento,
            EnumListagemPadraoDeTarefas listagemPadraoDeTarefas,
            EnumTelaInicial telaInicial,
            EnumPrimeiroDiaDaSemana primeiroDiaDaSemana,
            string email,
            string telefone)
        {
            NotificarTarefasAntesDoInicio = notificarTarefasAntesDoInicio;
            QuantidadeDateTimeAntesDoInicio = quantidadeDateTimeAntesDoInicio;
            Unidade = unidade;
            ReforcarlembreteNoProprioDia = reforcarlembreteNoProprioDia;
            ReceberNotificacaoPorEmail = receberNotificacaoPorEmail;
            ReceberNotificacaoPorWhatsApp = receberNotificacaoPorWhatsApp;
            ReceberResumoDiario = receberResumoDiario;
            HorarioResumoDiario = horarioResumoDiario;
            ArquivamentoAutomaticoTarefasConcluidas = arquivamentoAutomaticoTarefasConcluidas;
            ArquivarTarefasConcluidas = arquivarTarefasConcluidas;
            QuantidadeDiasParaArquivamento = quantidadeDiasParaArquivamento;
            ListagemPadraoDeTarefas = listagemPadraoDeTarefas;
            TelaInicial = telaInicial;
            PrimeiroDiaDaSemana = primeiroDiaDaSemana;
            Email = email;
            Telefone = telefone;
        }
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

    public enum EnumUnidadeTempo
    {
        Minutos = 0,
        Horas = 1,
        Dias = 2
    }
}

