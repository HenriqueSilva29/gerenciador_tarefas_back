using Application.Dtos.Subtarefas;

namespace Application.Services.ServSubTarefas
{
    public interface IServSubtarefa
    {
        Task AdicionarSubtarefa(AdicionarSubtarefaRequest dto);
    }
}
