using Application.Dtos.FiltroDtos;
using Application.Dtos.Filtros;
using Application.Dtos.Tarefas;
using Application.Services.ServTarefas;
using Application.Utils.Paginacao;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Atualizar(int id, [FromBody] UpdateTarefaRequest request)
        {
            await aplic.AtualizarTarefa(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await aplic.RemoverTarefa(id);
            return NoContent();
        }

        [HttpGet("tarefas-vencidas")]
        public async Task<ActionResult> RecuperarTarefasVencidas(int pagina, int quantidade)
        {
                var tarefas = await aplic.RecuperarTarefasVencidas(pagina, quantidade);
                return Ok(tarefas);
        }


        [HttpPost("{id}/atualizar-prioridade")]
        public async Task<ActionResult> AtualizarPrioridade(int id, [FromBody] UpdatePrioridadeTarefaRequest dto)
        {
            await aplic.AtualizarPrioridade(id, dto);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaResponse>> ObterPorId(int id)
        {
            var tarefa = await aplic.ObterPorId(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }
    }
}
