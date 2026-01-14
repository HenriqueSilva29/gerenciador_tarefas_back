using Application.Utils.Transacao;
using Repository.Repositorys.LembreteRep;

namespace Application.Services.ServLembretes
{
    public class ServLembrete : IServLembrete
    {
        private readonly IRepLembrete _rep;
        private readonly IUnitOfWork _unitOfWork;

        public ServLembrete(IRepLembrete rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task MarcarLembreteComoEnviado(Guid id)
        {
            var lembrete = await _rep.RecuperarPorGuid(id);
            if (lembrete is null)
                return;

            lembrete.MarcarComoEnviado();
            await _rep.Atualizar(lembrete);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
