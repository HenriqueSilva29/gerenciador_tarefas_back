using Infra.Mensageria.RabbitMQ.Publicadores;
using Repository.Repositorys.LembreteRep;

namespace Infra.Jobs.Hangfire.JobDeLembretes
{
    public class JobDeLembrete : IJobDeLembrete
    {
        private readonly IRepLembrete _rep;
        private readonly IPublicadorDeMensagens _publisher;

        public JobDeLembrete(IRepLembrete rep, IPublicadorDeMensagens publisher)
        {
            _rep = rep;
            _publisher = publisher;
        }

        public async Task ExecutarAsync(Guid lembreteId)
        {
            var lembrete = await _rep.RecuperarPorGuid(lembreteId);

            if (lembrete == null || lembrete.FoiEnviado)
                return;

            await _publisher.PublicarAsync(new
            {
                Id = lembrete.CodigoLembrete,
                Texto = lembrete.Texto,
                TarefaId = lembrete.CodigoToDoItem
            });

            lembrete.MarcarComoEnviado();
            await _rep.Atualizar(lembrete);
        }
    }
}
