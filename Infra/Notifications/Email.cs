using Application.Emails;
using Application.Interfaces.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infra.Notifications
{
    public class Email : IEmail
    {
        private readonly EmailOptions _options;
        private readonly ILogger<Email> _logger;

        public Email(IOptions<EmailOptions> options, ILogger<Email> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendAsync(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(message.To));
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new BodyBuilder
            {
                HtmlBody = message.IsHtml ? message.Body : null,
                TextBody = message.IsHtml ? null : message.Body
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            try
            {
                var socketOptions = _options.UseSsl
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.StartTls;

                await smtp.ConnectAsync(_options.Host, _options.Port, socketOptions);
                await smtp.AuthenticateAsync(_options.UserName, _options.Password);
                await smtp.SendAsync(mimeMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar email para {Destino}", message.To);
                throw;
            }
        }
    }
}
