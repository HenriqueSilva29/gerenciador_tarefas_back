using Application.Dtos.Usuarios;

namespace Application.Services.ServUsuarios
{
    public interface IServUsuario
    {
        public Task<UsuarioResponse> RegistrarUsuario(RegistrarUsuarioRequest dto);
        public Task AtualizarNomeUsuario(int id, AtualizarNomeUsuarioRequest dto);

    }
}
