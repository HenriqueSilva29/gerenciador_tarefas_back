using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys.UsuarioRep
{
    public class RepUsuario : Repository<Usuario, int>, IRepUsuario
    {
        public RepUsuario(ContextEF context) : base(context) { }

        public async Task<Usuario?> ObterUsuarioPorNome(string nome)
        {
            return await AsQueryable()
                .FirstOrDefaultAsync(u => u.Nome == nome);

        }
    }
}
