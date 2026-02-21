using Application.Emails;

namespace Application.Interfaces.Email
{
    public interface IEmail
    {
        Task SendAsync(EmailMessage message);
    }
}
