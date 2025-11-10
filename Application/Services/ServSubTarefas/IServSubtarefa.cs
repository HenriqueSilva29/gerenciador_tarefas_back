using Application.Dtos.SubtarefaDto;
using Application.Dtos.ToDoItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServSubTarefas
{
    public interface IServSubtarefa
    {
        Task AdicionarSubtarefa(AdicionarSubtarefaDto dto);
    }
}
