using Application.Funcionalidades.Usuarios.Dtos;

namespace Application.Funcionalidades.Usuarios.Contratos.CasosDeUso
{
    public interface IRegistrarUsuarioCasoDeUso
    {
        public Task<UsuarioResposta> ExecutarAsync(RegistrarUsuarioRequisicao dto);
    }
}

