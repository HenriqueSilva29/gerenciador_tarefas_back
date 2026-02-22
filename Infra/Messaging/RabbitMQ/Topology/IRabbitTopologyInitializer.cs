using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mensageria.RabbitMQ.Topology
{
    public interface IRabbitTopologyInitializer
    {
        Task InitializeAsync(IChannel channel);
    }
}
