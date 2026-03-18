using Application.Dtos.Filtros;
using Application.Dtos.PaginacaoDtos;
using Application.Utils.Ordenacao;
using Domain.Entities;
using System.Linq.Expressions;
using static Domain.Entities.Tarefa;

namespace Application.Dtos.FiltroDtos
{
    public class TarefaFiltroRequest : PaginacaoRequest, ITarefaFiltroRequest<Tarefa>, ISortHelper<Tarefa>
    {
        public string OrdenarPor { get; set; } = "Codigo";
        public string Direcao { get; set; } = "asc";

        public Dictionary<string, Expression<Func<Tarefa, object>>> ObterCamposOrdenaveis()
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

        public int? CodigoTarefa { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusTarefa? Status { get; set; }
        public EnumPrioridadeTarefa? Prioridade { get; set; }
        public EnumCategoriaTarefa? Categoria { get; set; }

        public Dictionary<string, Expression<Func<Tarefa, bool>>> ObterFiltros()
        {
            var filtros = new Dictionary<string, Expression<Func<Tarefa, bool>>>();

            if (CodigoTarefa.HasValue)
                filtros.Add("Codigo", x => x.Id == CodigoTarefa.Value);

            if (!string.IsNullOrWhiteSpace(Titulo))
                filtros.Add("Titulo", x => x.Titulo.Contains(Titulo));

            if (!string.IsNullOrEmpty(Descricao))
                filtros.Add("Descricao", x => x.Descricao == Descricao);

            if (DataCriacao.HasValue)
                filtros.Add("DataCriacao", x => x.DataCriacao.Value >= DataCriacao.Value);

            if (DataVencimento.HasValue)
                filtros.Add("DataVencimento", x => x.DataVencimento.Value <= DataVencimento.Value);

            if (Prioridade.HasValue)
                filtros.Add("Prioridade", x => x.Prioridade == Prioridade.Value);

            if (Categoria.HasValue)
                filtros.Add("Categoria", x => x.Categoria == Categoria.Value);

            return filtros;
        }
    }
}
