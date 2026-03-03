using Application.Interfaces.UseCases.Autenticacaos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infra.Autenticacao
{
    public class VerificarSenha : IVerificarSenhaUseCase
    {
        private readonly PasswordHasher<Usuario> _hasher = new();

        public bool Executar(Usuario usuario, string senha)
        {
            var resultado = _hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);
            return resultado == PasswordVerificationResult.Success;
        }
    }
}
