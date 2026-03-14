using Domain.Common.ValueObjects;

namespace Domain.Events.Tarefas
{
    public class TarefaCriadaEvent : IDomainEvent
    {
        public int IdTarefa {  get; }
        public UtcDateTime OcorreuEm { get; }

        public TarefaCriadaEvent(int tarefaId)
        {
            IdTarefa = tarefaId;
            OcorreuEm = UtcDateTime.Now();
        }
    }
}
