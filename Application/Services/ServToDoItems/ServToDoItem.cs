using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Mappers;
using Application.Services.ServToDoItems;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Application.Utils.Transacao;
using Application.Views;
using Domain.ToDoItems;
using Microsoft.EntityFrameworkCore;
using Repository.ToDoItemRep;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : IServToDoItem
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;

        public ServToDoItem(IRepToDoItem rep, IUnitOfWork unitOfWork) : base()
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginacaoHelper<ToDoItemView>> RecuperarTodos(int pagina = 1, int quantidade = 10)
        {
            var query =  _rep.AsQueryable();

            var queryMapeada = query.Select(t => MapToDoItem.MapearParaView(t));

            return await queryMapeada.PaginarAsync(pagina, quantidade);
        }

        public async Task Adicionar(AdicionarToDoItemDto dto)
        {
            var toDoItem = MapToDoItem.AdicionarToDoItemDto(dto);

            await _rep.Adicionar(toDoItem);

            _unitOfWork.CommitTransactionAsync();

        }

        public async Task Atualizar(int id, AtualizarToDoItemDto dto)
        {
            var ToDoItem = await _rep.RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = MapToDoItem.AtualizarToDoItemDto(ToDoItem, dto);

            await _rep.Atualizar(ToDoItem);

            _unitOfWork.CommitTransactionAsync();
        }

        public async Task Remover(int id)
        {
            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem is null) throw new Exception("Registro não encontrado");

            await _rep.Remover(toDoItem);

            _unitOfWork.CommitTransactionAsync();
        }
        
        public async Task<PaginacaoHelper<ToDoItem>> RecuperarTarefasVencidas(int pagina, int quantidade)
        {
                var dataHoje = DateTime.Now.Date;
                var query = _rep.AsQueryable()
                                .Where(t => t.DataVencimento < dataHoje);

                return await query.PaginarAsync(pagina,quantidade);
        }

        public async Task AtualizarPrioridade(int id, AtualizarPrioridadeDto dto)
        {
            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem == null)
                throw new Exception("Tarefa não encontrada.");

            toDoItem.DefinirPrioridade(dto.Prioridade);

            await _rep.Atualizar(toDoItem);

            _unitOfWork.CommitTransactionAsync();
        }

        public async Task<PaginacaoHelper<ToDoItem>> ListarFiltradoAsync(FiltroToDoItemDto parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.PaginarAsync(parametros.Pagina,parametros.QuantidadePorPagina);
        }
    }
}
