using System.Linq.Expressions;

namespace Application.Interfaces.Filtros
{
    public interface IBaseFiltroRequest<T>
    {
        public Dictionary<string, Expression<Func<T, bool>>> ObterFiltros();
    }
}
