using Application.Funcionalidades.Usuarios.Dtos;

namespace Application.Funcionalidades.Usuarios.Contratos.CasosDeUso
{
    public interface IAtualizarNomeUsuarioCasoDeUso
    {
        Task ExecutarAsync(int id, AtualizarNomeUsuarioRequisicao dto);
    }
}

