using Domain.Entidades;

namespace Repository.Repositorios.Notificacoes
{
    public interface IRepNotificacao : IRepositorio<Notificacao, int>
    {
        public IQueryable<Notificacao> QueryPorUsuario(int idUsuario);
        public Task<Notificacao?> ObterPorIdDoUsuarioAsync(int idNotificacao, int idUsuario);
        public Task<int> ContarNaoLidasDoUsuarioAsync(int idUsuario);
        public Task<List<Notificacao>> ListarNaoLidasDoUsuarioAsync(int idUsuario);
        public Task<List<Notificacao>> ListarPorUsuarioAsync(int idUsuario);
    }
}


