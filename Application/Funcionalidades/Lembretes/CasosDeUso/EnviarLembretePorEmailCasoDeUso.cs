using Application.Emails;
using Application.Interfaces.Email;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Domain.Entidades;
using Microsoft.Extensions.Logging;

namespace Application.Funcionalidades.Lembretes.CasosDeUso
{
    public class EnviarLembretePorEmailCasoDeUso : IEnviarLembretePorEmailCasoDeUso
    {
        private readonly ILogger<EnviarLembretePorEmailCasoDeUso> _logger;
        private readonly LembreteEmailCompose _lembreteEmailCompose;
        private readonly IEmail _email;

        public EnviarLembretePorEmailCasoDeUso
        (   
            ILogger<EnviarLembretePorEmailCasoDeUso> logger, 
            LembreteEmailCompose lembreteEmailCompose,
            IEmail email
        )
        {
            _logger = logger;
            _lembreteEmailCompose = lembreteEmailCompose;
            _email = email;
        }

        public async Task ExecuteAsync(Lembrete lembrete, ParamGeral paramGeral)
        {
            _logger.LogInformation("Iniciando montagem do email");

            var email =  _lembreteEmailCompose.Compose(lembrete, paramGeral);

            await _email.SendAsync(email);

            _logger.LogInformation("Email enviado");

        }
    }
}


