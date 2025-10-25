using System.Linq.Expressions;

namespace Application.Filtros
{
    
    public class Filtro<T> : IFiltro<T>
    {
        private readonly List<Expression<Func<T, bool>>> _filtros = new List<Expression<Func<T, bool>>>();

        public Filtro<T> AdicionarFiltro(Expression<Func<T, bool>> filtro)
        {
            _filtros.Add(filtro);
            return this;
        }

        public IEnumerable<T> ExecutarFiltroEnumerable(IEnumerable<T> source)
        {
            foreach (var filtro in _filtros)
            {
                source = source.Where(filtro.Compile()); 
            }
            return source;
        }

        public IQueryable<T> ExecutarFiltroQueryable(IQueryable<T> source)
        {
            foreach (var filtro in _filtros)
            {
                source = source.Where(filtro);
            }
            return source;
        }
    }
}
