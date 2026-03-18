using System.Linq.Expressions;

namespace Application.Dtos.Filtros
{
    public interface ITarefaFiltroRequest<T>
    {
        Dictionary<string, Expression<Func<T, bool>>> ObterFiltros();
    }
}
