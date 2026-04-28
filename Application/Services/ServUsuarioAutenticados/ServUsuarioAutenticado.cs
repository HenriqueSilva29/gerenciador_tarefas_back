using Application.Interfaces.UseCases.UsuarioAutenticados;
using Application.Services.ServUsuarioAutenticados;

namespace Application.Services.UsuarioAutenticados
{
    public class ServUsuarioAutenticado : IServUsuarioAutenticado
    {
        private readonly IObterIdUsuarioUseCase _obterIdUsuarioUseCase; 
        public ServUsuarioAutenticado
        (
            IObterIdUsuarioUseCase obterIdUsuarioUseCase
        )
        {
            _obterIdUsuarioUseCase = obterIdUsuarioUseCase;
        }
        public int ObterIdUsuarioLogado()
             => _obterIdUsuarioUseCase.Execute();
    }
}
