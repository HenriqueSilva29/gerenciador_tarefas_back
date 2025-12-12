using Domain.Lembretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServLembretes
{
    public interface IServLembrete
    {
        Task<Lembrete> CriarLembrete(int codigoToDoItem, DateTime data, string texto);
    }
}
