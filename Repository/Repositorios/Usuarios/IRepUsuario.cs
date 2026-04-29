using Domain.Entidades;

namespace Repository.Repositorios.Usuarios
{
    public interface IRepUsuario : IRepositorio<Usuario, int>
    {
        Task<Usuario?> ObterUsuarioPorEmail(string email);
    }
}


