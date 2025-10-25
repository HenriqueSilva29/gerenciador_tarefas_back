
namespace Application.Filtros
{
    public interface IFiltro<T>
    {
        IEnumerable<T> ExecutarFiltroEnumerable(IEnumerable<T> source);
        IQueryable<T> ExecutarFiltroQueryable(IQueryable<T> source);
    }
}
