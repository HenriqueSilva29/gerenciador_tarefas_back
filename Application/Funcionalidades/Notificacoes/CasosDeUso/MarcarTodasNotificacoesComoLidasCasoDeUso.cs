using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class MarcarTodasNotificacoesComoLidasCasoDeUso : IMarcarTodasNotificacoesComoLidasCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public MarcarTodasNotificacoesComoLidasCasoDeUso(
            IRepNotificacao repNotificacao,
            IUnitOfWork unitOfWork,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task ExecuteAsync()
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var notificacoes = await _repNotificacao.ListarNaoLidasDoUsuarioAsync(idUsuario);

            if (notificacoes.Count == 0)
                return;

            await _unitOfWork.BeginTransactionAsync();

            foreach (var notificacao in notificacoes)
            {
                notificacao.MarcarComoLida();
                _repNotificacao.Atualizar(notificacao);
            }

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}



