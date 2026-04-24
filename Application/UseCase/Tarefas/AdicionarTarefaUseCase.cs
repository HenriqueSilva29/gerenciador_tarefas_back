using Application.Dtos.Tarefas;
using Application.Events.Tarefas;
using Application.Interfaces.UseCases.Lembretes;
using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Infra.Messaging.RabbitMQ.Publicadores;
using Repository.Repositorys.ParamGeralRep;
using Repository.TarefaRep;

public class AdicionarTarefaUseCase : IAdicionarTarefaUseCase
{
    private readonly IRepTarefa _rep;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitEventPublisher _publisher;

    public AdicionarTarefaUseCase(
        IRepTarefa rep,
        IUnitOfWork unitOfWork,
        IRabbitEventPublisher publisher)
    {
        _rep = rep;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<TarefaResponse> Executar(CreateTarefaRequest dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var Tarefa = MapTarefa.ToTarefa(dto);

        _rep.Adicionar(Tarefa);

        await _unitOfWork.CommitTransactionAsync();

        await _publisher.PublishAsync(new TarefaCriadaEvent(Tarefa.Id));

        return new TarefaResponse { Id = Tarefa.Id};

    }
}