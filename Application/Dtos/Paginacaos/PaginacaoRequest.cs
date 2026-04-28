namespace Application.Dtos.PaginacaoDtos
{
    public class PaginacaoRequest
    {
        public int Pagina { get; set; } = 1;
        public int QuantidadePorPagina { get; set; } = 10;
    }
}
