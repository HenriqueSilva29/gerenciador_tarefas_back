using Application.Dtos;
using Application.Dtos.Filtros;
using Application.Interfaces.IToDoItems;
using Application.Services.Mappers.ToDoItems;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : Service<ToDoItem>, IServToDoItem
    {
        public ServToDoItem(IRepository<ToDoItem> repository) : base(repository)
        {

        }

        public async Task<IEnumerable<ToDoItem>> RecuperarTarefas()
        {
            return await RecuperarTodos();
        }

        public async Task Adicionar(ToDoItemDto dto)
        {
            var ToDoItem = new ToDoItem();

            if (dto == null) throw new Exception("Objeto nulo");

            ToDoItem = new MapToDoItem().Mapear(ToDoItem, dto);

            await Adicionar(ToDoItem);
        }

        public async Task Atualizar(int id, ToDoItemDto dto)
        {

            var ToDoItem = await RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = new MapToDoItem().Mapear(ToDoItem, dto);

            await Atualizar(ToDoItem);
        }

        public async Task Remover(int id)
        {
            var toDoItem = await RecuperarPorId(id);

            if (toDoItem is null) throw new Exception("Registro não encontrado");

            await Remover(toDoItem);
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(FiltroToDoItemDto parametros)
        {

            var query = _rep.AsQueryable();

            if (!string.IsNullOrEmpty(parametros.Titulo))
                query = query.Where(x => x.Titulo.Contains(parametros.Titulo));

            if (!string.IsNullOrEmpty(parametros.Descricao))
                query = query.Where(x => x.Descricao.Contains(parametros.Descricao));

            if (parametros.DataCriacao.HasValue)
                query = query.Where(x => x.DataCriacao >= parametros.DataCriacao.Value);

            if (parametros.DataVencimento.HasValue)
                query = query.Where(x => x.DataVencimento <= parametros.DataVencimento.Value);

            if (parametros.Prioridade.HasValue)
                query = query.Where(x => x.Prioridade == parametros.Prioridade.Value);

            if (parametros.Categoria.HasValue)
                query = query.Where(x => x.Categoria == parametros.Categoria.Value);

            return await query.ToListAsync();
        }

    }
}
