using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class Tarefa : IEntityId<int>
    {
        public Tarefa()
        {
            Status = EnumStatusTarefa.NaoIniciada;
            SubTarefas = new List<Tarefa>();
            Lembretes = new List<Lembrete>();

        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public UtcDateTime DataCriacao { get; set; }
        public UtcDateTime DataVencimento { get; set; }
        public DateOnly DataTarefa { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public EnumStatusTarefa Status { get; private set ; }
        public EnumPrioridadeTarefa Prioridade { get; set; }
        public EnumCategoriaTarefa Categoria { get; set; }
        public int? CodigoUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        //autorreferente
        public int? CodigoTarefaPai { get; set; }
        public Tarefa? TarefaPai { get; set; }
        public List<Tarefa> SubTarefas { get; set; }

        public List<Lembrete> Lembretes { get; set; }

        public enum EnumCategoriaTarefa
        {
            Trabalho = 0,
            Pessoal = 1,
            Estudo = 2,
            Lazer = 3,
            Compras = 4,
            Casa = 5,
            Saude = 6,
            Outros = 7
        }

        public enum EnumPrioridadeTarefa
        {
            Baixa = 0,
            Media = 1,
            Alta = 2
        }
        public enum EnumStatusTarefa
        {
            NaoIniciada = 0,
            EmProgresso = 1,
            Concluida = 2,
            Cancelada = 3,
            Atrasada = 4,
            Pausada = 5,
            AguardandoRevisao = 6,
            Adiada = 7
        }

        public enum TipoEventoHistoricoTarefa
        {
            TarefaCriada,
            TarefaAtualizada,
            TarefaConcluida,
            TarefaReaberta,
            TarefaExcluida,
            SubtarefaCriada,
            SubtarefaAtualizada,
            SubtarefaConcluida,
            SubtarefaReaberta,
            SubtarefaExcluida,
            SubtarefaVinculada,
            SubtarefaDesvinculada
        }

        public void CancelarTarefa()
        {
            Status = EnumStatusTarefa.Cancelada;
        }

        public void DefinirPrioridade(EnumPrioridadeTarefa prioridade)
        {
            Prioridade = prioridade;
        }

        public bool PossuiSubtarefaNaoConcluida()
        {
            return SubTarefas.Any(st => st.Status != EnumStatusTarefa.Concluida);
        }

        public bool PodeConcluirTarefa()
        {
            return !PossuiSubtarefaNaoConcluida();
        }

        public void AtualizarStatus(EnumStatusTarefa status)
        {
            this.Status = status;
        }

        public void ConcluirTarefa()
        {
            if (!PodeConcluirTarefa())
                throw new ExceptionDomain(
                    EnumCodigosDeExcecao.PossuiSubtarefaNaoFinalizada,
                    "Necessário finalizar as subtarefas pendentes",
                    StatusCodes.Status409Conflict);

            Status = EnumStatusTarefa.Concluida;
        }

        public bool PossuiSubtarefaComPrioridadeMaior()
        {
            return SubTarefas.Any(st => st.Prioridade > Prioridade);
        }

        public bool ValidaPrioridadeParaSubtarefa()
        {
            if (PossuiSubtarefaComPrioridadeMaior())
                throw new ExceptionDomain(
                    EnumCodigosDeExcecao.PossuiSubtarefaComPrioridadeMaiorQueTarefaPai,
                    "Subtarefa foi definida com prioridade maior que tarefa pai",
                    StatusCodes.Status409Conflict);

            return true;
        }

        public int CalcularNivelPrioridade(Tarefa tarefa)
        {
            var hoje = UtcDateTime.Now();

            if (tarefa.DataVencimento.Value < hoje.Value)
                return 1;

            if (tarefa.DataVencimento.Value == hoje.Value)
                return 2;

            if (tarefa.DataVencimento.Value <= hoje.Value.AddDays(3))
                return 3;

            return 4;
        }
    }
}
