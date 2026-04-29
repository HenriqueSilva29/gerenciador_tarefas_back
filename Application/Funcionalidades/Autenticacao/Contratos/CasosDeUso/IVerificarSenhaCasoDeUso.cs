

using Domain.Entidades;

namespace Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso
{
    public interface IVerificarSenhaCasoDeUso
    {
        public bool Executar(Usuario usuario, string senha);
    }
}


