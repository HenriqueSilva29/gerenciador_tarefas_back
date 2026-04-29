using Application.Funcionalidades.Usuarios.Dtos;
using Application.Funcionalidades.Usuarios.Contratos.CasosDeUso;

namespace Application.Funcionalidades.Usuarios.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        readonly IRegistrarUsuarioCasoDeUso _registrarUsuarioUseCase;
        readonly IAtualizarNomeUsuarioCasoDeUso _atualizarNomeUsuarioUseCase;

        public ServicoUsuario
        (
            IRegistrarUsuarioCasoDeUso registrarUsuarioUseCase,
            IAtualizarNomeUsuarioCasoDeUso atualizarNomeUsuarioUseCase
        )
        {
            _registrarUsuarioUseCase = registrarUsuarioUseCase;
            _atualizarNomeUsuarioUseCase = atualizarNomeUsuarioUseCase;
        }

        public Task<UsuarioResposta> RegistrarUsuario(RegistrarUsuarioRequisicao dto)
            => _registrarUsuarioUseCase.ExecutarAsync(dto);

        public Task AtualizarNomeUsuario(int id, AtualizarNomeUsuarioRequisicao dto)
            => _atualizarNomeUsuarioUseCase.ExecutarAsync(id, dto);

    }
}

