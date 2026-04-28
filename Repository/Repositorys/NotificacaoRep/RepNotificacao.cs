using Domain.Entities;
using Repository.ContextEFs;

namespace Repository.Repositorys.NotificacaoRep
{
    public class RepNotificacao : Repository<Notificacao, int>, IRepNotificacao
    {
        public RepNotificacao(ContextEF context) : base(context)
        {
        }
    }
}
