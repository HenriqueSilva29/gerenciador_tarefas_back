using Application.Interfaces.UseCases.Notificacoes;
using Application.Services.ServUsuarioAutenticados;
using Application.Utils.Transacao;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class ExcluirTodasNotificacoesUseCase : IExcluirTodasNotificacoesUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServUsuarioAutenticado _servUsuarioAutenticado;

        public ExcluirTodasNotificacoesUseCase(
            IRepNotificacao repNotificacao,
            IUnitOfWork unitOfWork,
            IServUsuarioAutenticado servUsuarioAutenticado)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task ExecuteAsync()
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var notificacoes = await _repNotificacao.AsQueryable()
                .Where(n => n.CodigoUsuario == idUsuario)
                .ToListAsync();

            if (notificacoes.Count == 0)
                return;

            await _unitOfWork.BeginTransactionAsync();

            foreach (var notificacao in notificacoes)
            {
                _repNotificacao.Remover(notificacao);
            }

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
