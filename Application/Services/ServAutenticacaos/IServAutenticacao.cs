using Application.Dtos.Autenticacaos;

namespace Application.Services.ServAutenticacaos
{
    public interface IServAutenticacao
    {
        public Task<string> Login(RequestAutenticacaoRequest request);
    }
}
