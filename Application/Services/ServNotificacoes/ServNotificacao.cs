using Application.Dtos.Filtros.Notificacoes;
using Application.Dtos.Notificacoes;
using Application.Interfaces.UseCases.Notificacoes;
using Application.Utils.Paginacao;

namespace Application.Services.ServNotificacoes
{
    public class ServNotificacao : IServNotificacao
    {
        private readonly IListarNotificacoesUseCase _listarNotificacoesUseCase;
        private readonly IContarNotificacoesNaoLidasUseCase _contarNotificacoesNaoLidasUseCase;
        private readonly IMarcarNotificacaoComoLidaUseCase _marcarNotificacaoComoLidaUseCase;
        private readonly IMarcarTodasNotificacoesComoLidasUseCase _marcarTodasNotificacoesComoLidasUseCase;
        private readonly IExcluirNotificacaoUseCase _excluirNotificacaoUseCase;
        private readonly IExcluirTodasNotificacoesUseCase _excluirTodasNotificacoesUseCase;

        public ServNotificacao(
            IListarNotificacoesUseCase listarNotificacoesUseCase,
            IContarNotificacoesNaoLidasUseCase contarNotificacoesNaoLidasUseCase,
            IMarcarNotificacaoComoLidaUseCase marcarNotificacaoComoLidaUseCase,
            IMarcarTodasNotificacoesComoLidasUseCase marcarTodasNotificacoesComoLidasUseCase,
            IExcluirNotificacaoUseCase excluirNotificacaoUseCase,
            IExcluirTodasNotificacoesUseCase excluirTodasNotificacoesUseCase)
        {
            _listarNotificacoesUseCase = listarNotificacoesUseCase;
            _contarNotificacoesNaoLidasUseCase = contarNotificacoesNaoLidasUseCase;
            _marcarNotificacaoComoLidaUseCase = marcarNotificacaoComoLidaUseCase;
            _marcarTodasNotificacoesComoLidasUseCase = marcarTodasNotificacoesComoLidasUseCase;
            _excluirNotificacaoUseCase = excluirNotificacaoUseCase;
            _excluirTodasNotificacoesUseCase = excluirTodasNotificacoesUseCase;
        }

        public Task<PaginacaoHelper<NotificacaoResponse>> Listar(NotificacaoFiltroRequest filtro)
            => _listarNotificacoesUseCase.ExecuteAsync(filtro);

        public Task<int> ContarNaoLidas()
            => _contarNotificacoesNaoLidasUseCase.ExecuteAsync();

        public Task MarcarComoLida(int id)
            => _marcarNotificacaoComoLidaUseCase.ExecuteAsync(id);

        public Task MarcarTodasComoLidas()
            => _marcarTodasNotificacoesComoLidasUseCase.ExecuteAsync();

        public Task Excluir(int id)
            => _excluirNotificacaoUseCase.ExecuteAsync(id);

        public Task ExcluirTodas()
            => _excluirTodasNotificacoesUseCase.ExecuteAsync();
    }
}
