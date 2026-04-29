
using Domain.Entidades;

namespace Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso
{
    public interface IGerarTokenCasoDeUso 
    {
        public string Executar(Usuario usuario);
    }
}


