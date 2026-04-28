using Application.Interfaces.Context;
using Application.Interfaces.UseCases.Notificacoes;
using Application.Services.ServUsuarioAutenticados;
using Application.Utils.Transacao;
using Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class MarcarTodasNotificacoesComoLidasUseCase : IMarcarTodasNotificacoesComoLidasUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServUsuarioAutenticado _servUsuarioAutenticado;

        public MarcarTodasNotificacoesComoLidasUseCase(
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
                .Where(n => n.CodigoUsuario == idUsuario && !n.Lida)
                .ToListAsync();

            if (notificacoes.Count == 0)
                return;

            var dataLeitura = UtcDateTime.Now();

            await _unitOfWork.BeginTransactionAsync();

            foreach (var notificacao in notificacoes)
            {
                notificacao.Lida = true;
                notificacao.DataLeitura = dataLeitura;
                _repNotificacao.Atualizar(notificacao);
            }

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
