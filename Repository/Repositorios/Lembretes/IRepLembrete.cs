using Domain.Entidades;

namespace Repository.Repositorios.Lembretes
{
    public interface IRepLembrete : IRepositorio<Lembrete, int>
    {
        public Task<List<Lembrete>> ObterPendentesParaDisparo(DateTimeOffset agora);
        public Task<Lembrete?> ObterComTarefaPorIdAsync(int id);
    }
}


