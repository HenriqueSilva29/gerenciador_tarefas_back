
namespace Application.Filtros
{
    public interface IFiltro<T>
    {
        Task<IEnumerable<T>> AplicarFiltroEnumerable(IEnumerable<T> source);
        Task<IQueryable<T>> AplicarFiltroQueryable(IQueryable<T> source);
    }
}
