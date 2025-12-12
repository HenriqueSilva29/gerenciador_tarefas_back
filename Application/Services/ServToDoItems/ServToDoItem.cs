using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Mappers;
using Application.Services.ServToDoItems;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Views;
using Domain.ToDoItems;
using Microsoft.EntityFrameworkCore;
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


        
        public async Task<List<ToDoItem>> RecuperarTarefasVencidas()
        {
            try
            {
                var query = _rep.AsQueryable();
                
                //Implementar consultas ao banco

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

        public async Task<List<ToDoItem>> ListarFiltradoAsync(FiltroToDoItemDto parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.ToListAsync();
        }
    }
}
