using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PaginacaoDtos
{
    public class PaginacaoRequest
    {
        public int Pagina { get; set; } = 1;
        public int QuantidadePorPagina { get; set; } = 10;
    }
}
