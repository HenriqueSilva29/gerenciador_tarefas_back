using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Channels
{
    public interface IRabbitChannelFactory
    {
        Task<IChannel> CreateChannelAsync();

    }
}
