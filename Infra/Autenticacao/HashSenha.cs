using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Domain.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Infra.Autenticacao
{
    public class HashSenha : IHashSenhaCasoDeUso
    {
        private readonly PasswordHasher<Usuario> _hasher = new();
        public string Executar(string senha)
        {
            return _hasher.HashPassword(null, senha);
        }
    }
}

