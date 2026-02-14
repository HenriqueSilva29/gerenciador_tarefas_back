namespace Application.Interfaces.Messaging
{
    public interface IMessageConsumer
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
