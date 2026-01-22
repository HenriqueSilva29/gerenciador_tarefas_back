namespace Application.Services.ServLembretes
{
    public interface IServLembrete
    {
        Task AgendarLembrete(Guid id);
    }
}
