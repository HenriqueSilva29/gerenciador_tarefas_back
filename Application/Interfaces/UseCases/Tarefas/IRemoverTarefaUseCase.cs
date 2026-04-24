namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IRemoverTarefaUseCase
    {
        public Task Executar(int id);
    }
}
