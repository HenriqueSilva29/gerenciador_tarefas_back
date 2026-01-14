using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ
{
    public interface IRabbitChannelFactory
    {
        Task<IChannel> CreateChannelAsync();
    }
}
