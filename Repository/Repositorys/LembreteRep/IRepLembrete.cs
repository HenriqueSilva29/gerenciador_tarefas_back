using Domain.Entities.Lembretes;
using Repository.Repositorys.GuidRep;
using Repository.Repositorys.IntRep;

namespace Repository.Repositorys.LembreteRep
{
    public interface IRepLembrete : IRepInt<Lembrete>
    {
        public Task<List<Lembrete>> ObterPendentesParaDisparo(DateTimeOffset agora);
    }
}
