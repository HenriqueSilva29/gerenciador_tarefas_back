using Domain.Comum;
using Domain.Comum.ObjetosDeValor;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;

namespace Domain.Entidades
{
    public class Tarefa : IEntidadeId<int>
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

        public void VincularUsuario(int codigoUsuario)
        {
            CodigoUsuario = codigoUsuario;
        }

        public void AtualizarStatus(EnumStatusTarefa status)
        {
            this.Status = status;
        }

    }
}

