using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class ExcluirTodasNotificacoesCasoDeUso : IExcluirTodasNotificacoesCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public ExcluirTodasNotificacoesCasoDeUso(
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

            var notificacoes = await _repNotificacao.ListarPorUsuarioAsync(idUsuario);

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



