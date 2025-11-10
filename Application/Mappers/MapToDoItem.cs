using Application.Dtos.ToDoItemDtos;
using Application.Views;
using Domain.ToDoItems;

namespace Application.Mappers
{
    public static class MapToDoItem
    {
        public static ToDoItem Mapear(ToDoItem ToDoItem, AdicionarToDoItemDto dto)
        {
            ToDoItem.Titulo = dto.Titulo;
            ToDoItem.Descricao = dto.Descricao;
            ToDoItem.DataCriacao = DateTime.Now;
            ToDoItem.DataVencimento = dto.DataVencimento;
            ToDoItem.Status = dto.Status;
            ToDoItem.Categoria = dto.Categoria;
            ToDoItem.Prioridade = dto.Prioridade;

            return ToDoItem;
        }

        public static ToDoItem AdicionarToDoItemDto(AdicionarToDoItemDto dto)
        {
            return new ToDoItem
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = DateTime.Now,
                DataVencimento = dto.DataVencimento,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria
            };
        }

        public static ToDoItem AtualizarToDoItemDto(ToDoItem toDoItem, AtualizarToDoItemDto dto)
        {
            toDoItem.Titulo = dto.Titulo;
            toDoItem.Descricao = dto.Descricao;
            toDoItem.DataVencimento = dto.DataVencimento;
            toDoItem.Prioridade = dto.Prioridade;
            toDoItem.Categoria = dto.Categoria;
            toDoItem.Status = dto.Status;
            return toDoItem;
        }

        public static ToDoItemView MapearParaView(ToDoItem toDoItem)
        {
            return new ToDoItemView
            {
                CodigoToDoItem = toDoItem.CodigoToDoItem,
                Titulo = toDoItem.Titulo,
                Descricao = toDoItem.Descricao,
                DataCriacao = toDoItem.DataCriacao,
                DataVencimento = toDoItem.DataVencimento,
                Status = toDoItem.Status,
                Prioridade = toDoItem.Prioridade,
                Categoria = toDoItem.Categoria,
                CodigoToDoItemPai = toDoItem.CodigoToDoItemPai,
                SubTarefas = toDoItem.SubTarefas?.Select(st => st.CodigoToDoItem).ToList()
            };
        }
    }
}
