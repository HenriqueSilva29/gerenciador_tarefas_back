

using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;

namespace Repository.Repositorios.ParamGerais
{
    public class RepParamGeral : Repositorio<ParamGeral,int>, IRepParamGeral 
    {
        public RepParamGeral(ContextEF context) : base(context){ }

        public async Task<ParamGeral?> ObterPorUsuarioAsync(int idUsuario)
        {
            return await AsQueryable()
                .FirstOrDefaultAsync(p => p.CodigoUsuario == idUsuario);
        }
    }
}


