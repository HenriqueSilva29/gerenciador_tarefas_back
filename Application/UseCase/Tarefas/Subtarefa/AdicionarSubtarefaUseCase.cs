using Application.Dtos.Tarefas;
using Application.Dtos.Tarefas.Subtarefas;
using Application.Events.Tarefas;
using Application.Interfaces.UseCases.Tarefas.Subtarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Infra.Messaging.RabbitMQ.Publicadores;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas.Subtarefa
{
    public class AdicionarSubtarefaUseCase : IAdicionarSubtarefaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepTarefa _rep;
        private readonly IRabbitEventPublisher _publisher;

        public AdicionarSubtarefaUseCase
        (
            IUnitOfWork unitOfWork,
            IRepTarefa rep,
            IRabbitEventPublisher publisher
        )
        {
            _unitOfWork = unitOfWork;
            _rep = rep;
            _publisher = publisher;
        }
        public async Task<SubtarefaResponse> Executar(AdicionarSubtarefaRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var Tarefa = MapSubtarefa.ParaTarefa(dto);

            _rep.Adicionar(Tarefa);

            await _unitOfWork.CommitTransactionAsync();

            await _publisher.PublishAsync(new TarefaCriadaEvent(Tarefa.Id));

            return new SubtarefaResponse { Id = Tarefa.Id };
        }
    }
}
