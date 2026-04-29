using Application.Dtos.Paginacaos;
using Application.Interfaces.Filtros;
using Application.Utils.Ordenacao;
using Domain.Entidades;
using System.Linq.Expressions;
using static Domain.Entidades.Tarefa;

namespace Application.Funcionalidades.Tarefas.Filtros
{
    public class TarefaFiltroRequisicao : PaginacaoRequisicao, IBaseFiltroRequisicao<Tarefa>, IAuxiliarOrdenacao<Tarefa>
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
                { "Prioridade", x => x.Prioridade },
                { "Categoria", x => x.Categoria }
            };
        }

        public int? CodigoTarefa { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public EnumStatusTarefa? Status { get; set; }
        public EnumPrioridadeTarefa? Prioridade { get; set; }
        public EnumCategoriaTarefa? Categoria { get; set; }

        public int? CodigoTarefaPai { get; set; }
        public bool? ApenasTarefasPai { get; set; }
        public bool? ApenasSubtarefas { get; set; }

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

            if (Prioridade.HasValue)
                filtros.Add("Prioridade", x => x.Prioridade == Prioridade.Value);

            if (Categoria.HasValue)
                filtros.Add("Categoria", x => x.Categoria == Categoria.Value);

            if (CodigoTarefaPai.HasValue)
                filtros.Add("CodigoTarefaPai", x => x.CodigoTarefaPai == CodigoTarefaPai.Value);

            if (ApenasTarefasPai == true)
                filtros.Add("ApenasTarefasPai", x => x.CodigoTarefaPai == null);

            if (ApenasSubtarefas == true)
                filtros.Add("ApenasSubtarefas", x => x.CodigoTarefaPai != null);

            return filtros;
        }
    }
}



