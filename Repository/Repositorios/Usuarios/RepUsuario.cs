using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;

namespace Repository.Repositorios.Usuarios
{
    public class RepUsuario : Repositorio<Usuario, int>, IRepUsuario
    {
        public RepUsuario(ContextEF context) : base(context) { }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return await AsQueryable()
                .FirstOrDefaultAsync(u => u.Email == email);

        }
    }
}


