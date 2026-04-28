using Application.Dtos.Notificacoes;
using Application.Interfaces.UseCases.Notificacoes;
using Application.Utils.Transacao;
using Domain.Common.ValueObjects;
using Domain.Entities;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class CriarNotificacaoUseCase : ICriarNotificacaoUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;

        public CriarNotificacaoUseCase(IRepNotificacao repNotificacao, IUnitOfWork unitOfWork)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
        }

        public async Task<Notificacao> ExecuteAsync(CriarNotificacaoRequest dto)
        {
            var notificacao = new Notificacao
            {
                CodigoUsuario = dto.CodigoUsuario,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Mensagem = dto.Mensagem,
                DataCriacao = UtcDateTime.Now(),
                Lida = false,
                DataLeitura = null
            };

            await _unitOfWork.BeginTransactionAsync();
            _repNotificacao.Adicionar(notificacao);
            await _unitOfWork.CommitTransactionAsync();

            return notificacao;
        }
    }
}
