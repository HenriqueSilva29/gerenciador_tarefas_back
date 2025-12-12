using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Jobs.Hangfire.JobDeLembretes
{
    public interface IJobDeLembrete
    {
        Task ExecutarAsync(Guid lembreteId);
    }
}
