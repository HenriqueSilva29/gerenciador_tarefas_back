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
        public async Task AgendarLembrete(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var lembrete = await _rep.RecuperarPorGuid(id);
            if (lembrete is null)
            {
                Console.WriteLine("Lembrete não encontrado");
                return;
            }

            lembrete.Agendar();
            await _rep.Atualizar(lembrete);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
