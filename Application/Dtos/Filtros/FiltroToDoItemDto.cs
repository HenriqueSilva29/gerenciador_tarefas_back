using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Filtros
{
    public class FiltroToDoItemDto
    {
        public int? CodigoToDoItem { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem? Status { get; set; }
        public EnumPrioridadeToDoItem? Prioridade { get; set; }
        public EnumCategoriaToDoItem? Categoria { get; set; }
    }
}
