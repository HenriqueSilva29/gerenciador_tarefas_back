using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.Tarefas;
using Application.Dtos.Tarefas.Subtarefas;
using Application.Services.ServTarefas;
using Application.Utils.Paginacao;
using Microsoft.AspNetCore.Mvc;
using Repository.QueryModels.Tarefas;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly IServTarefa aplic;

        public TarefaController(IServTarefa servTarefa)
        {
            aplic = servTarefa;
        }

        [HttpGet("listar-tarefas")]
        public async Task<ActionResult<PaginacaoHelper<TarefaResponse>>> Listar([FromQuery] TarefaFiltroRequest filtro)
        {
            var result = await aplic.ListarTarefas(filtro);
            return Ok(result);
        }

        [HttpPost("criar-tarefa")]
        public async Task<ActionResult<TarefaResponse>> Criar([FromBody] CreateTarefaRequest request)
        {
            var result = await aplic.AdicionarTarefa(request);

            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] int id, [FromBody] UpdateTarefaRequest request)
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
        public async Task<ActionResult> AtualizarPrioridade([FromRoute] int id, [FromBody] UpdatePrioridadeTarefaRequest request)
        {
            await aplic.AtualizarPrioridade(id, request);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> AtualizarStatus([FromRoute] int id, [FromBody] UpdateStatusTarefaRequest request)
        {
            await aplic.AtualizarStatus(id, request);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaResponse>> ObterPorId([FromRoute] int id)
        {
            var tarefa = await aplic.ObterPorId(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPost("subtarefa")]
        public async Task<ActionResult<SubtarefaResponse>> Criar(AdicionarSubtarefaRequest request)
        {
            var result = await aplic.AdicionarSubtarefa(request);

            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

        [HttpGet("{id}/historico")]
        public async Task<ActionResult<HistoricoTarefaItemQueryModel>> Historico([FromRoute] int id)
        {
            var historico = await aplic.RecuperarHistoricoPorId(id);

            if (historico == null)
                return NotFound();

            return Ok(historico);
        }
    }
}
