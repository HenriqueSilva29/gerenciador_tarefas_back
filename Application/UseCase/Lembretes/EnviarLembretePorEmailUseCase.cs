using Application.Emails;
using Application.Interfaces.Email;
using Application.Interfaces.UseCases.Lembretes;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Lembretes
{
    public class EnviarLembretePorEmailUseCase : IEnviarLembretePorEmailUseCase
    {
        private readonly ILogger<EnviarLembretePorEmailUseCase> _logger;
        private readonly LembreteEmailCompose _lembreteEmailCompose;
        private readonly IEmail _email;

        public EnviarLembretePorEmailUseCase
        (   
            ILogger<EnviarLembretePorEmailUseCase> logger, 
            LembreteEmailCompose lembreteEmailCompose,
            IEmail email
        )
        {
            _logger = logger;
            _lembreteEmailCompose = lembreteEmailCompose;
            _email = email;
        }

        public async Task ExecuteAsync(Lembrete lembrete, string emailDestinatario)
        {
            _logger.LogInformation("Iniciando montagem do email");

            var email =  _lembreteEmailCompose.Compose(lembrete, emailDestinatario);

            await _email.SendAsync(email);

            _logger.LogInformation("Email enviado");

        }
    }
}
