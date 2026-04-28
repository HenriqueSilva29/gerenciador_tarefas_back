using Application.Interfaces.Context;
using Application.Interfaces.UseCases.Notificacoes;
using Application.Interfaces.UseCases.UsuarioAutenticados;
using Application.Services.ServUsuarioAutenticados;
using Application.Utils.Transacao;
using Domain.Common.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class MarcarNotificacaoComoLidaUseCase : IMarcarNotificacaoComoLidaUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServUsuarioAutenticado _servUsuarioAutenticado;

        public MarcarNotificacaoComoLidaUseCase(
            IRepNotificacao repNotificacao,
            IUnitOfWork unitOfWork,
            IServUsuarioAutenticado servUsuarioAutenticado)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task ExecuteAsync(int id)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var notificacao = await _repNotificacao.AsQueryable()
                .FirstOrDefaultAsync(n => n.Id == id && n.CodigoUsuario == idUsuario);

            if (notificacao is null)
            {
                throw new ExceptionApplication(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    "Notificacao nao encontrada.",
                    StatusCodes.Status404NotFound);
            }

            if (notificacao.Lida)
                return;

            notificacao.MarcarComoLida();

            await _unitOfWork.BeginTransactionAsync();
            _repNotificacao.Atualizar(notificacao);
            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
