using Domain.Entities;

namespace Repository.Repositorys.UsuarioRep
{
    public interface IRepUsuario : IRepository<Usuario, int>
    {
        Task<Usuario?> ObterUsuarioPorEmail(string email);
    }
}
