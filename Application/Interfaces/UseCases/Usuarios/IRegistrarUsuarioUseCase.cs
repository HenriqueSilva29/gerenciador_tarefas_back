using Application.Dtos.UsuarioDtos;

namespace Application.Interfaces.UseCases.Usuarios
{
    public interface IRegistrarUsuarioUseCase
    {
        public Task Executar(RegistrarUsuarioDto dto);
    }
}
