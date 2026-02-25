using Domain.Entities;

namespace Repository.Repositorys.LembreteRep
{
    public interface IRepLembrete : IRepository<Lembrete, int>
    {
        public Task<List<Lembrete>> ObterPendentesParaDisparo(DateTimeOffset agora);
    }
}
