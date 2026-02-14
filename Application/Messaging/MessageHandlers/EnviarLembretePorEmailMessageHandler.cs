using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Application.UseCase.Lembrete;

namespace Application.Massaging.MessageHandlers
{
    public class EnviarLembretePorEmailMessageHandler : IMessageHandler<LembreteMensagemDto>
    {
        private readonly EnviarLembretePorEmailUseCase _enviarLembretePorEmailUseCase;

        public EnviarLembretePorEmailMessageHandler(EnviarLembretePorEmailUseCase enviarLembretePorEmailUseCase)
        {
            _enviarLembretePorEmailUseCase = enviarLembretePorEmailUseCase;
        }

        public async Task HandleAsync
        (
            LembreteMensagemDto message,
            CancellationToken cancellationToken
        )
        {
            await _enviarLembretePorEmailUseCase.ExecuteAsync(message);
        }

    }
}
