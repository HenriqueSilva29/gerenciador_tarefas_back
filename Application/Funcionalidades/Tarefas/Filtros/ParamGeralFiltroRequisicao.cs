using Application.Dtos.Paginacaos;
using Application.Interfaces.Filtros;
using Application.Utils.Ordenacao;
using Domain.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Application.Funcionalidades.Tarefas.Filtros
{
    public class ParamGeralFiltroRequisicao : PaginacaoRequisicao, IBaseFiltroRequisicao<ParamGeral>, IAuxiliarOrdenacao<ParamGeral>
    {
        public string OrdenarPor { get; set; } = "Codigo";
        public string Direcao { get; set; } = "asc";

        public int? CodigoParamGeral { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }

        public Dictionary<string, Expression<Func<ParamGeral, object>>> ObterCamposOrdenaveis()
        {
            return new()
            {
                { "Codigo", x => x.Id }
            };
        }

        public Dictionary<string, Expression<Func<ParamGeral, bool>>> ObterFiltros()
        {
            var filtros = new Dictionary<string, Expression<Func<ParamGeral, bool>>>();

            if (CodigoParamGeral.HasValue)
                filtros.Add("Codigo", x => x.Id == CodigoParamGeral.Value);

            if(!Email.IsNullOrEmpty())
                filtros.Add("Codigo", x => x.Email == Email);

            if(!Telefone.IsNullOrEmpty())
                filtros.Add("Codigo", x => x.Telefone == Telefone);

            return filtros;
        }
    }
}



