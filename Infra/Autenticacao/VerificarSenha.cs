using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Domain.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Infra.Autenticacao
{
    public class VerificarSenha : IVerificarSenhaCasoDeUso
    {
        private readonly PasswordHasher<Usuario> _hasher = new();

        public bool Executar(Usuario usuario, string senha)
        {
            var resultado = _hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);
            return resultado == PasswordVerificationResult.Success;
        }
    }
}

