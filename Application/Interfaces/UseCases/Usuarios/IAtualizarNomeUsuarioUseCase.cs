using Application.Dtos.Usuarios;

namespace Application.Interfaces.UseCases.Usuarios
{
    public interface IAtualizarNomeUsuarioUseCase
    {
        Task ExecutarAsync(int id, AtualizarNomeUsuarioRequest dto);
    }
}
