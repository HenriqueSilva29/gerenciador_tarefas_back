using Application.Emails;
using Application.Interfaces.Email;
using Microsoft.Extensions.Logging;

namespace Infra.Emails
{
    public class Email : IEmail
    {
        private readonly ILogger<Email> _logger;
        public Email(ILogger<Email> logger)
        {
            _logger = logger;
        }
        public Task SendAsync(EmailMessage message)
        {
            _logger.LogInformation("Enviando...");

            return Task.CompletedTask;
        }
    }
}
