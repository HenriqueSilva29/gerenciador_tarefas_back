using Application.Dtos.Usuarios;

namespace Application.Interfaces.UseCases.Usuarios
{
    public interface IRegistrarUsuarioUseCase
    {
        public Task<UsuarioResponse> Executar(RegistrarUsuarioRequest dto);
    }
}
