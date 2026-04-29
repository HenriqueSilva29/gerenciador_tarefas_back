using Application.Funcionalidades.Usuarios.Dtos;

namespace Application.Funcionalidades.Usuarios.Servicos
{
    public interface IServicoUsuario
    {
        public Task<UsuarioResposta> RegistrarUsuario(RegistrarUsuarioRequisicao dto);
        public Task AtualizarNomeUsuario(int id, AtualizarNomeUsuarioRequisicao dto);

    }
}

