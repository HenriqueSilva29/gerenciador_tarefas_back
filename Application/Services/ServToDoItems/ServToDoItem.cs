using Application.Dtos;
using Application.Dtos.Filtros.FiltroToDoItemDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Filtros;
using Application.Services.Mappers.ToDoItems;
using Application.Services.ServToDoItems;
using Application.Services.ServUtils;
using Domain.ToDoItems;
using Infra.Utils.SortHelperUtils;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : IServToDoItem
    {
        private readonly IRepository<ToDoItem> _rep;

        public ServToDoItem(IRepository<ToDoItem> repository) : base()
        {
            _rep = repository;
        }

        public async Task<IEnumerable<ToDoItem>> RecuperarTodos()
        {
            return await _rep.RecuperarTodos();
        }

        public async Task Adicionar(ToDoItemDto dto)
        {
            var toDoItem = new ToDoItem();

            if (dto == null) throw new Exception("Objeto nulo");

            toDoItem = new MapToDoItem().Mapear(toDoItem, dto);

            await _rep.Adicionar(toDoItem);
        }

        public async Task Atualizar(int id, ToDoItemDto dto)
        {

            var ToDoItem = await _rep.RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = new MapToDoItem().Mapear(ToDoItem, dto);

            await _rep.Atualizar(ToDoItem);
        }

        public async Task Remover(int id)
        {
            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem is null) throw new Exception("Registro não encontrado");

            await _rep.Remover(toDoItem);
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(FiltroToDoItemDto parametros)
        {
            return await AplicarFiltro(parametros);
        }

        private async Task<IEnumerable<ToDoItem>> AplicarFiltro(FiltroToDoItemDto parametros)
        {            
            var query = _rep.AsQueryable();

            query = MontarFiltro(parametros).ExecutarFiltroQueryable(query);
            query = AplicarOrdenacao(query, parametros);

            return await query.ToListAsync();
        }

        private IQueryable<ToDoItem> AplicarOrdenacao(IQueryable<ToDoItem> query, FiltroToDoItemDto parametros)
        {
            var (colunaOrdenada, ascDescOrdenada) = ServUtil.DefinirParametrosOrdenacao(parametros);
            query = SortHelperUtil<ToDoItem>.ExecutarOrdenacao(query, colunaOrdenada, ascDescOrdenada);

            return query;
        }

        private Filtro<ToDoItem> MontarFiltro(FiltroToDoItemDto parametros)
        {
            var filtro = new Filtro<ToDoItem>();

            if (parametros.CodigoToDoItem.HasValue)
                filtro.AdicionarFiltro(x => x.CodigoToDoItem == parametros.CodigoToDoItem);

            if (!string.IsNullOrEmpty(parametros.Titulo))
                filtro.AdicionarFiltro(x => x.Titulo.Contains(parametros.Titulo));

            if (!string.IsNullOrEmpty(parametros.Descricao))
                filtro.AdicionarFiltro(x => x.Descricao.Contains(parametros.Descricao));

            if (parametros.DataCriacao.HasValue)
                filtro.AdicionarFiltro(x => x.DataCriacao >= parametros.DataCriacao.Value);

            if (parametros.DataVencimento.HasValue)
                filtro.AdicionarFiltro(x => x.DataVencimento <= parametros.DataVencimento.Value);

            if (parametros.Prioridade.HasValue)
                filtro.AdicionarFiltro(x => x.Prioridade == parametros.Prioridade.Value);

            if (parametros.Categoria.HasValue)
                filtro.AdicionarFiltro(x => x.Categoria == parametros.Categoria.Value);

            return filtro;
        }
    }
}
