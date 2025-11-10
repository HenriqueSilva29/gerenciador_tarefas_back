using Application.Dtos;
using Application.Dtos.Filtros;
using Application.Dtos.ToDoItemDtos;
using Application.Filtros;
using Application.Mappers;
using Application.Services.ServToDoItems;
using Application.Services.ServUtils;
using Application.Views;
using Domain.ToDoItems;
using Infra.Utils.SortHelperUtils;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys;
using Repository.ToDoItemRep;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : IServToDoItem
    {
        private readonly IRepToDoItem _rep;

        public ServToDoItem(IRepToDoItem rep) : base()
        {
            _rep = rep;
        }

        public async Task<List<ToDoItemView>> RecuperarTodos()
        {
            var listaDeTarefas = await _rep.RecuperarTodos();

            var listaDeTarefasMapeadas = listaDeTarefas.Select(t => MapToDoItem.MapearParaView(t)).ToList();

            return listaDeTarefasMapeadas;
        }

        public async Task Adicionar(AdicionarToDoItemDto dto)
        {
            var toDoItem = MapToDoItem.AdicionarToDoItemDto(dto);

            await _rep.Adicionar(toDoItem);
        }

        public async Task Atualizar(int id, AtualizarToDoItemDto dto)
        {
            var ToDoItem = await _rep.RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = MapToDoItem.AtualizarToDoItemDto(ToDoItem, dto);

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
        
        public async Task<List<ToDoItem>> RecuperarTarefasVencidas()
        {
            try
            {
                var query = _rep.AsQueryable();
                var filtro = new Filtro<ToDoItem>();

                var dataAtual = DateTime.Today;

                filtro.AdicionarFiltro(x => x.DataVencimento < dataAtual);

                query = filtro.ExecutarFiltroQueryable(query);

                return await query.ToListAsync();
            }
            catch(Exception e)
            {
               throw new Exception(e.Message);
            }

        }

        public async Task AtualizarPrioridade(int id, AtualizarPrioridadeDto dto)
        {
            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem == null)
                throw new Exception("Tarefa não encontrada.");

            toDoItem.DefinirPrioridade(dto.Prioridade);

            await _rep.Atualizar(toDoItem);
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
