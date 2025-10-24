using System;
using System.Collections.Generic;
using System.Linq;
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

        // Dados já carregados na memória
        public async Task<IEnumerable<T>> AplicarFiltroEnumerable(IEnumerable<T> source)
        {
            foreach(var filtro in _filtros)
            {
                source = source.Where(filtro.Compile());
            }

            return await Task.FromResult(source);
        }

        // Consulta a banco de dados
        public async Task<IQueryable<T>> AplicarFiltroQueryable(IQueryable<T> source)
        {
            foreach (var filtro in _filtros)
            {
                source = source.Where(filtro);
            }

            return await Task.FromResult(source);
        }
    }
}
