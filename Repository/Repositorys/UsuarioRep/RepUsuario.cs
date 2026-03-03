using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys.UsuarioRep
{
    public class RepUsuario : Repository<Usuario, int>, IRepUsuario
    {
        public RepUsuario(ContextEF context) : base(context) { }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return await AsQueryable()
                .FirstOrDefaultAsync(u => u.Email == email);

        }
    }
}
