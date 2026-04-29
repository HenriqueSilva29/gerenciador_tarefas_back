using Application.Dtos.Paginacaos;
using Application.Interfaces.Filtros;
using Application.Utils.Ordenacao;
using Domain.Entidades;
using System.Linq.Expressions;

namespace Application.Funcionalidades.Notificacoes.Filtros
{
    public class NotificacaoFiltroRequisicao : PaginacaoRequisicao, IBaseFiltroRequisicao<Notificacao>, IAuxiliarOrdenacao<Notificacao>
    {

        public int? CodigoNotificacao { get; set; }
        public EnumTipoNotificacao? Tipo { get; set; }
        public string? Titulo { get; set; }
        public bool? Lida { get; set; }
        public string OrdenarPor { get; set; } = "DataCriacao";
        public string Direcao { get; set; } = "desc";

        public Dictionary<string, Expression<Func<Notificacao, object>>> ObterCamposOrdenaveis()
        {
            return new()
            {
                { "Codigo", x => x.Id },
                { "Tipo", x => x.Tipo },
                { "Titulo", x => x.Titulo },
                { "Lida", x => x.Lida },
                { "DataCriacao", x => x.DataCriacao }
            };
        }

        public Dictionary<string, Expression<Func<Notificacao, bool>>> ObterFiltros()
        {
            var filtros = new Dictionary<string, Expression<Func<Notificacao, bool>>>();

            if (CodigoNotificacao.HasValue)
                filtros.Add("Codigo", x => x.Id == CodigoNotificacao.Value);

            if (Tipo.HasValue)
                filtros.Add("Tipo", x => x.Tipo == Tipo.Value);

            if (!string.IsNullOrWhiteSpace(Titulo))
                filtros.Add("Titulo", x => x.Titulo.Contains(Titulo));

            if (Lida.HasValue)
                filtros.Add("Lida", x => x.Lida == Lida.Value);

            return filtros;
        }
    }
}



