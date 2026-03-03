using Application.Dtos.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.ServAutenticacaos
{
    public interface IServAutenticacao
    {
        public Task<string> Login(RequestAutenticacaoDto request);
    }
}
