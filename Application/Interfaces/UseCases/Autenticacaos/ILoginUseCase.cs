using Application.Dtos.Autenticacao;

namespace Application.Interfaces.UseCases.Autenticacaos
{
    public interface ILoginUseCase
    {
        public Task<string> Executar(RequestAutenticacaoDto request);
    }
}
