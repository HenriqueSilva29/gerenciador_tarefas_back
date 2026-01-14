using Application.Dtos.Filtros;
using Application.Dtos.PaginacaoDtos;
using Application.Utils.Ordenacao;
using Domain.Entities.ToDoItems;
using System.Linq.Expressions;
using static Domain.Entities.ToDoItems.ToDoItem;

namespace Application.Dtos.FiltroDtos
{
    public class FiltroToDoItemDto : PaginacaoDto, IFiltroDto<ToDoItem>, ISortHelper<ToDoItem>
    {
        public string OrdenarPor { get; set; } = "Codigo";
        public string Direcao { get; set; } = "asc";

        public Dictionary<string, Expression<Func<ToDoItem, object>>> ObterCamposOrdenaveis()
        {
            return new()
            {
                { "Codigo", x => x.Id },
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
                filtros.Add("Codigo", x => x.Id == CodigoToDoItem.Value);

            if (!string.IsNullOrWhiteSpace(Titulo))
                filtros.Add("Titulo", x => x.Titulo.Contains(Titulo));

            if (!string.IsNullOrEmpty(Descricao))
                filtros.Add("Prioridade", x => x.Descricao == Descricao);

            if (DataCriacao.HasValue)
                filtros.Add("DataCriacao", x => x.DataCriacao.Value >= DataCriacao.Value);

            if (DataVencimento.HasValue)
                filtros.Add("DataVencimento", x => x.DataVencimento.Value <= DataVencimento.Value);

            if (Prioridade.HasValue)
                filtros.Add("Prioridade", x => x.Prioridade == Prioridade.Value);

            if (Categoria.HasValue)
                filtros.Add("Prioridade", x => x.Categoria == Categoria.Value);

            return filtros;
        }
    }
}
