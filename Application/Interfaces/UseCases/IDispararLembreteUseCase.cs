using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface IDispararLembreteUseCase 
    {
        Task ExecuteAsync(Guid lembreteId);
    }
}
