using Application.Interfaces.UseCases.Notificacoes;
using Application.Utils.Transacao;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class ExcluirNotificacaoUseCase : IExcluirNotificacaoUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirNotificacaoUseCase(IRepNotificacao repNotificacao, IUnitOfWork unitOfWork)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            var notificacao = await _repNotificacao.RecuperarPorIdAsync(id);

            if (notificacao == null)  await Task.CompletedTask;

            await _unitOfWork.BeginTransactionAsync();
            _repNotificacao.Remover(notificacao);
            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
