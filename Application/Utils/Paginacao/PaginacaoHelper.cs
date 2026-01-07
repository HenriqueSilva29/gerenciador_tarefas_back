
namespace Application.Utils.Paginacao
{
    public class PaginacaoHelper<T>
    {
        public IEnumerable<T> Itens { get; set; }
        public int PaginaAtual { get; set; }
        public int QuantidadePorPagina { get; set; }
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }

        public PaginacaoHelper(IEnumerable<T> itens, int paginaAtual, int quantidadePorPagina, int totalItens)
        {
            Itens = itens;
            PaginaAtual = paginaAtual;
            QuantidadePorPagina = quantidadePorPagina;
            TotalItens = totalItens;
            TotalPaginas = (int)Math.Ceiling(totalItens / (double)quantidadePorPagina);
        }
    }
}
