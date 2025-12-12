using Application.Dtos.Filtros;
using Application.Utils.Ordenacao;
using Domain.Enums.EnumToDoItem;
using Domain.ToDoItems;
using System.Linq.Expressions;

namespace Application.Dtos.FiltroDtos
{
    public class FiltroToDoItemDto : IFiltroDto<ToDoItem>, ISortHelper<ToDoItem>
    {
        public string OrdenarPor { get; set; } = "Codigo";
        public string Direcao { get; set; } = "asc";

        public Dictionary<string, Expression<Func<ToDoItem, object>>> ObterCamposOrdenaveis()
        {
            return new()
            {
                { "Codigo", x => x.CodigoToDoItem },
                { "Titulo", x => x.Titulo },
                { "Descricao", x => x.Descricao },
                { "DataCriacao", x => x.DataCriacao },
                { "Status", x => x.Status },
                { "DataVencimento", x => x.DataVencimento },
                { "Prioridade", x => x.Prioridade },
                { "Categoria", x => x.Categoria }
            };
        }

        public int? CodigoToDoItem { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem? Status { get; set; }
        public EnumPrioridadeToDoItem? Prioridade { get; set; }
        public EnumCategoriaToDoItem? Categoria { get; set; }

        public Dictionary<string, Expression<Func<ToDoItem, bool>>> ObterFiltros()
        {
            var filtros = new Dictionary<string, Expression<Func<ToDoItem, bool>>>();

            if (CodigoToDoItem.HasValue)
                filtros.Add("Codigo", x => x.CodigoToDoItem == CodigoToDoItem.Value);

            if (!string.IsNullOrWhiteSpace(Titulo))
                filtros.Add("Titulo", x => x.Titulo.Contains(Titulo));

            if (!string.IsNullOrEmpty(Descricao))
                filtros.Add("Prioridade", x => x.Descricao == Descricao);

            if (DataCriacao.HasValue)
                filtros.Add("DataCriacao", x => x.DataCriacao >= DataCriacao.Value);

            if (DataVencimento.HasValue)
                filtros.Add("DataVencimento", x => x.DataVencimento <= DataVencimento.Value);

            if (Prioridade.HasValue)
                filtros.Add("Prioridade", x => x.Prioridade == Prioridade.Value);

            if (Categoria.HasValue)
                filtros.Add("Prioridade", x => x.Categoria == Categoria.Value);

            return filtros;
        }
    }
}
