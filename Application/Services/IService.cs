using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> RecuperarTodos();
        Task<T> RecuperarPorId(int id);
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Remover(T entity);
        Task<IEnumerable<T>> Filtrar(Expression<Func<T, bool>> filtro);
    }
}
