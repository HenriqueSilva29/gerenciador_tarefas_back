namespace Application.Interfaces.Messaging
{
    public interface IMessageDispatcher
    {
        Task DispatchAsync(string json);
    }
}
