using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;

namespace Repository.Repositorios.Notificacoes
{
    public class RepNotificacao : Repositorio<Notificacao, int>, IRepNotificacao
    {
        public RepNotificacao(ContextEF context) : base(context)
        {
        }

        public IQueryable<Notificacao> QueryPorUsuario(int idUsuario)
        {
            return AsQueryable().Where(n => n.CodigoUsuario == idUsuario);
        }

        public async Task<Notificacao?> ObterPorIdDoUsuarioAsync(int idNotificacao, int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .FirstOrDefaultAsync(n => n.Id == idNotificacao);
        }

        public async Task<int> ContarNaoLidasDoUsuarioAsync(int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .CountAsync(n => !n.Lida);
        }

        public async Task<List<Notificacao>> ListarNaoLidasDoUsuarioAsync(int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .Where(n => !n.Lida)
                .ToListAsync();
        }

        public async Task<List<Notificacao>> ListarPorUsuarioAsync(int idUsuario)
        {
            return await QueryPorUsuario(idUsuario)
                .ToListAsync();
        }
    }
}


