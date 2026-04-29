using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class MarcarNotificacaoComoLidaCasoDeUso : IMarcarNotificacaoComoLidaCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public MarcarNotificacaoComoLidaCasoDeUso(
            IRepNotificacao repNotificacao,
            IUnitOfWork unitOfWork,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task ExecuteAsync(int id)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var notificacao = await _repNotificacao.ObterPorIdDoUsuarioAsync(id, idUsuario);

            if (notificacao is null)
            {
                throw new ExcecaoAplicacao(
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



