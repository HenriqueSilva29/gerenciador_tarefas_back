using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;
using Repository.Repositorios;

namespace Repository.Repositorios.Tarefas
{
    public class RepTarefa : Repositorio<Tarefa, int>, IRepTarefa
    {
        public RepTarefa(ContextEF context) : base(context)
        {
        }

        public async Task<List<Tarefa>> RecuperarSubtarefasVinculadasAhTarefa(int idTarefaPai)
        {
            return await AsQueryable()
                            .OrderBy(t => t.DataCriacao)
                            .Where(t => t.CodigoTarefaPai == idTarefaPai)
                            .ToListAsync();
        }

        public async Task<List<Tarefa>> RecuperarSubtarefasVinculadasAhTarefa(int idTarefaPai, int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                            .OrderBy(t => t.DataCriacao)
                            .Where(t => t.CodigoTarefaPai == idTarefaPai)
                            .ToListAsync();
        }

        public IQueryable<Tarefa> QueryPorUsuario(int idUsuario)
        {
            return AsQueryable().Where(t => t.CodigoUsuario == idUsuario);
        }

        public async Task<Tarefa?> ObterPorIdDoUsuarioAsync(int idTarefa, int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .FirstOrDefaultAsync(t => t.Id == idTarefa);
        }

        public async Task<bool> ExistePorIdDoUsuarioAsync(int idTarefa, int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .AnyAsync(t => t.Id == idTarefa);
        }
    }
}


