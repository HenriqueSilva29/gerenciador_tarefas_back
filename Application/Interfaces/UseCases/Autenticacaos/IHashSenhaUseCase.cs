using Domain.Entities;

namespace Application.Interfaces.UseCases.Autenticacaos
{
    public interface IHashSenhaUseCase
    {
        public string Executar(string senha);
    }
}
