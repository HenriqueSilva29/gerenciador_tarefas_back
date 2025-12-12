using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Filtros
{
    public interface IFiltroDto<T>
    {
        Dictionary<string, Expression<Func<T, bool>>> ObterFiltros();
    }
}
