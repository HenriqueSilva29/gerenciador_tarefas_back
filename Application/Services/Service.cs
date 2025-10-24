using Domain.ToDoItem;
using Repository.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class Service<T> : IService<T> where T : class
    {
        public readonly IRepository<T> _rep;

        public Service(IRepository<T> repository)
        {
            _rep = repository;
        }

        public async Task<IEnumerable<T>> RecuperarTodos()
        {
            return await _rep.RecuperarTodos();
        }

        public async Task<T> RecuperarPorId(int id)
        {
            return await _rep.RecuperarPorId(id);
        }

        public async Task Adicionar(T entity)
        {
            await _rep.Adicionar(entity);
        }

        public async Task Atualizar(T entity)
        {
            await _rep.Atualizar(entity);
        }

        public async Task Remover(T entity)
        {
            await _rep.Remover(entity);
 
        }

        public async Task<IEnumerable<T>> Filtrar(Expression<Func<T, bool>> filtro)
        {
            return await _rep.Filtrar(filtro);
        }
    }
}
