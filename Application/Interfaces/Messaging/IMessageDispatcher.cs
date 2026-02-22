using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Messaging
{
    public interface IMessageDispatcher
    {
        Task DispatchAsync(string json);
    }
}
