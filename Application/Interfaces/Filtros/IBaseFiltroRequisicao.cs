using System.Linq.Expressions;

namespace Application.Interfaces.Filtros
{
    public interface IBaseFiltroRequisicao<T>
    {
        public Dictionary<string, Expression<Func<T, bool>>> ObterFiltros();
    }
}

