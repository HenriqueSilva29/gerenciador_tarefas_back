namespace Application.Services.ServLembretes
{
    public interface IServLembrete
    {
        Task MarcarLembreteComoEnviado(Guid id);
    }
}
