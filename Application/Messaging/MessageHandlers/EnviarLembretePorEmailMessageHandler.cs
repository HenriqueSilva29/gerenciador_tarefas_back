using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases.Lembretes;
using Repository.Repositorys.LembreteRep;

public class EnviarLembretePorEmailMessageHandler
    : IMessageHandler<LembreteVencimentoAtingidoEvent>
{
    public EnviarLembretePorEmailMessageHandler(IEnviarLembretePorEmailUseCase useCase)
    {
        _useCase = useCase;
    }

    private readonly IEnviarLembretePorEmailUseCase _useCase;

    public async Task HandleAsync(LembreteVencimentoAtingidoEvent evento)
    {
        await _useCase.ExecuteAsync(evento.idLembrete);
    }
        
}