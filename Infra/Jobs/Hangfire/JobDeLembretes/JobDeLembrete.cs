using Application.Dtos.LembreteDtos;
using Application.Utils.Transacao;
using Domain.Entities.Lembretes;
using Infra.Mensageria.RabbitMQ.Publicadores;
using Repository.Repositorys.LembreteRep;

namespace Infra.Jobs.Hangfire.JobDeLembretes
{
    public class JobDeLembrete : IJobDeLembrete
    {
        private readonly IRepLembrete _rep;
        private readonly IPublicadorDeMensagens _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public JobDeLembrete(
                            IRepLembrete rep, 
                            IPublicadorDeMensagens publisher,
                            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecutarAsync(Guid lembreteId)
        {
            await _unitOfWork.BeginTransactionAsync();

            var lembrete = await _rep.RecuperarPorGuid(lembreteId);

            if (lembrete == null)
                return;

            if (lembrete.Status != Lembrete.LembreteStatus.Pendente)
                return;

            await _publisher.PublicarAsync(new LembreteMensagemDto
            {
                IdLembrete = lembrete.Id,
                Texto = lembrete.Texto,
                IdTarefa = lembrete.CodigoToDoItem
            });

            lembrete.MarcarComoEnviado();
            await _rep.Atualizar(lembrete);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
