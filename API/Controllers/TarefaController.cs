using Application.Funcionalidades.Tarefas.Filtros;
using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;
using Application.Funcionalidades.Tarefas.Servicos;
using Application.Utils.Paginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.ModelosConsulta.Tarefas;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly IServicoTarefa aplic;

        public TarefaController(IServicoTarefa servTarefa)
        {
            aplic = servTarefa;
        }

        [HttpGet("listar-tarefas")]
        public async Task<ActionResult<PaginacaoHelper<TarefaResposta>>> Listar([FromQuery] TarefaFiltroRequisicao filtro)
        {
            var result = await aplic.ListarTarefas(filtro);
            return Ok(result);
        }

        [HttpPost("criar-tarefa")]
        public async Task<ActionResult<TarefaResposta>> Criar([FromBody] CriarTarefaRequisicao request)
        {
            var result = await aplic.AdicionarTarefa(request);

            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarTarefaRequisicao request)
        {
            await aplic.AtualizarTarefa(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover([FromRoute] int id)
        {
            await aplic.RemoverTarefa(id);
            return NoContent();
        }


        [HttpPost("{id}/atualizar-prioridade")]
        public async Task<ActionResult> AtualizarPrioridade([FromRoute] int id, [FromBody] AtualizarPrioridadeTarefaRequisicao request)
        {
            await aplic.AtualizarPrioridade(id, request);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> AtualizarStatus([FromRoute] int id, [FromBody] AtualizarStatusTarefaRequisicao request)
        {
            await aplic.AtualizarStatus(id, request);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaResposta>> ObterPorId([FromRoute] int id)
        {
            var tarefa = await aplic.ObterPorId(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPost("subtarefa")]
        public async Task<ActionResult<SubtarefaResposta>> Criar(AdicionarSubtarefaRequisicao request)
        {
            var result = await aplic.AdicionarSubtarefa(request);

            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

        [HttpGet("{id}/historico")]
        public async Task<ActionResult<HistoricoTarefaItemConsultaModelo>> Historico([FromRoute] int id)
        {
            var historico = await aplic.RecuperarHistoricoPorId(id);

            if (historico == null)
                return NotFound();

            return Ok(historico);
        }
    }
}




