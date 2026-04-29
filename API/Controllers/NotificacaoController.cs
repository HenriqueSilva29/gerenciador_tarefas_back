using Application.Funcionalidades.Notificacoes.Filtros;
using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Servicos;
using Application.Utils.Paginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly IServicoNotificacao _aplic;

        public NotificacaoController(IServicoNotificacao aplic)
        {
            _aplic = aplic;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<PaginacaoHelper<NotificacaoResposta>>> Listar([FromQuery] NotificacaoFiltroRequisicao filtro)
        {
            var result = await _aplic.Listar(filtro);
            return Ok(result);
        }

        [HttpGet("nao-lidas/total")]
        public async Task<ActionResult<int>> ContarNaoLidas()
        {
            var result = await _aplic.ContarNaoLidas();
            return Ok(result);
        }

        [HttpPatch("{id}/marcar-como-lida")]
        public async Task<ActionResult> MarcarComoLida([FromRoute] int id)
        {
            await _aplic.MarcarComoLida(id);
            return NoContent();
        }

        [HttpPatch("marcar-todas-como-lidas")]
        public async Task<ActionResult> MarcarTodasComoLidas()
        {
            await _aplic.MarcarTodasComoLidas();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir([FromRoute] int id)
        {
            await _aplic.Excluir(id);

            return NoContent();
        }

        [HttpDelete("todas")]
        public async Task<ActionResult> ExcluirTodas()
        {
            await _aplic.ExcluirTodas();

            return NoContent();
        }
    }
}


