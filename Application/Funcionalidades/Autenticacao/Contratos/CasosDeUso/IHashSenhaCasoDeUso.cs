using Domain.Entidades;

namespace Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso
{
    public interface IHashSenhaCasoDeUso
    {
        public string Executar(string senha);
    }
}


