using Domain.Enums.EnumToDoItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ToDoItemDtos
{
    public class AtualizarPrioridadeDto
    {
        public EnumPrioridadeToDoItem Prioridade { get; set; }
    }
}
