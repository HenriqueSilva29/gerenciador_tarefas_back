using Application.Dtos;

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
