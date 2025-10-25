using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServUtils
{
    public static class ServUtil
    {
        public static (string ColunaOrdenada, string AscDescOrdenada) DefinirParametrosOrdenacao(SortHelperDto parametros)
        {
            var colunaOrdenada = string.IsNullOrEmpty(parametros.ColunaOrdenada) ? "" : parametros.ColunaOrdenada;
            var ascDescOrdenada = string.IsNullOrEmpty(parametros.AscDescOrdenada) ? "asc" : parametros.AscDescOrdenada.ToLower();

            return (colunaOrdenada, ascDescOrdenada);
        }
    }
}
