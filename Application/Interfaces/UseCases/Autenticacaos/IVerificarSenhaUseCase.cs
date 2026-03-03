

using Domain.Entities;

namespace Application.Interfaces.UseCases.Autenticacaos
{
    public interface IVerificarSenhaUseCase
    {
        public bool Executar(Usuario usuario, string senha);
    }
}
