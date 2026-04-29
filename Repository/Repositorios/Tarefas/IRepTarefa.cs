using Domain.Entidades;
using Repository.Repositorios;

namespace Repository.Repositorios.Tarefas
{
    public interface IRepTarefa : IRepositorio<Tarefa, int>
    {
        // Métodos reutilizáveis devem serem inseridos no rep genérico
        public Task<List<Tarefa>> RecuperarSubtarefasVinculadasAhTarefa(int idTarefaPai);
        public Task<List<Tarefa>> RecuperarSubtarefasVinculadasAhTarefa(int idTarefaPai, int idUsuario);
        public IQueryable<Tarefa> QueryPorUsuario(int idUsuario);
        public Task<Tarefa?> ObterPorIdDoUsuarioAsync(int idTarefa, int idUsuario);
        public Task<bool> ExistePorIdDoUsuarioAsync(int idTarefa, int idUsuario);
    }
}


