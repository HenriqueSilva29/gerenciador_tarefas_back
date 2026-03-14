using Domain.Entities;
using Repository.Repositorys;

namespace Repository.TarefaRep
{
    public interface IRepTarefa : IRepository<Tarefa, int>
    {
        // Métodos reutilizáveis devem serem inseridos no rep genérico
    }
}
