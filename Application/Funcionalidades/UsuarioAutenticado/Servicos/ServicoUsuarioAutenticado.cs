using Application.Funcionalidades.UsuarioAutenticado.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;

namespace Application.Funcionalidades.UsuarioAutenticado.Servicos
{
    public class ServicoUsuarioAutenticado : IServicoUsuarioAutenticado
    {
        private readonly IObterIdUsuarioCasoDeUso _obterIdUsuarioUseCase; 
        public ServicoUsuarioAutenticado
        (
            IObterIdUsuarioCasoDeUso obterIdUsuarioUseCase
        )
        {
            _obterIdUsuarioUseCase = obterIdUsuarioUseCase;
        }
        public int ObterIdUsuarioLogado()
             => _obterIdUsuarioUseCase.Execute();
    }
}

