using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Channels
{
    public interface IRabbitChannelFactory
    {
        Task<IChannel> CreateChannelAsync();

    }
}
