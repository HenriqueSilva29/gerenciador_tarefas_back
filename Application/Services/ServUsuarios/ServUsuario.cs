using Application.Dtos.Usuarios;
using Application.Interfaces.UseCases.Usuarios;

namespace Application.Services.ServUsuarios
{
    public class ServUsuario : IServUsuario
    {
        readonly IRegistrarUsuarioUseCase _registrarUsuarioUseCase;
        readonly IAtualizarNomeUsuarioUseCase _atualizarNomeUsuarioUseCase;

        public ServUsuario
        (
            IRegistrarUsuarioUseCase registrarUsuarioUseCase,
            IAtualizarNomeUsuarioUseCase atualizarNomeUsuarioUseCase
        )
        {
            _registrarUsuarioUseCase = registrarUsuarioUseCase;
            _atualizarNomeUsuarioUseCase = atualizarNomeUsuarioUseCase;
        }

        public Task<UsuarioResponse> RegistrarUsuario(RegistrarUsuarioRequest dto)
            => _registrarUsuarioUseCase.ExecutarAsync(dto);

        public Task AtualizarNomeUsuario(int id, AtualizarNomeUsuarioRequest dto)
            => _atualizarNomeUsuarioUseCase.ExecutarAsync(id, dto);

    }
}
