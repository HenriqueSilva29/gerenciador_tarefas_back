using Application.Dtos;
using Domain.ToDoItem;

namespace Application.Services.Mappers.ToDoItems
{
    public class MapToDoItem
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int Prioridade { get; set; }
        public string Categoria { get; set; }

        public ToDoItem Mapear(ToDoItem ToDoItem, ToDoItemDto dto)
        {
            ToDoItem.Titulo = dto.Titulo;
            ToDoItem.Descricao = dto.Descricao;
            ToDoItem.DataCriacao = dto.DataCriacao;
            ToDoItem.DataVencimento = dto.DataVencimento;
            ToDoItem.Status = dto.status;
            ToDoItem.Categoria = dto.Categoria;
            ToDoItem.Prioridade = dto.Prioridade;

            return ToDoItem;
        }
    }
}
