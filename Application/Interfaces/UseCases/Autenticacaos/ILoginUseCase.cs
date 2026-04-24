using Application.Dtos.Autenticacaos;

namespace Application.Interfaces.UseCases.Autenticacaos
{
    public interface ILoginUseCase
    {
        public Task<string> Executar(RequestAutenticacaoRequest request);
    }
}
