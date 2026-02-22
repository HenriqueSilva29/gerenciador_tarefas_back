namespace Application.Interfaces.Messaging
{
    public interface IMessageHandler<IMessage>
    {
        Task HandleAsync(IMessage message, CancellationToken cancellationToken);
    }
}
