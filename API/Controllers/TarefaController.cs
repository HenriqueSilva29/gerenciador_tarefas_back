using API.Errors;
using Application.Dtos.FiltroDtos;
using Application.Dtos.TarefaDtos;
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
        public async Task<ActionResult<PaginacaoHelper<Tarefa>>> Listar([FromQuery] FiltroTarefaDto filtroTarefaDto)
        {
            var filteredItems = await aplic.ListarTarefas(filtroTarefaDto);
            return Ok(filteredItems);
        }

        [HttpPost("criar-tarefa")]
        public async Task<ActionResult> CriarTarefa([FromBody] AdicionarTarefaDto TarefaDto)
        {
                await aplic.AdicionarTarefa(TarefaDto);
                return Ok();  
        }

        [HttpPut("{id}/atualizar-tarefa")]
        public async Task<ActionResult> Atualizar([FromQuery] int id, [FromBody] AtualizarTarefaDto TarefaDto)
        {
            await aplic.AtualizarTarefa(id, TarefaDto);
            return NoContent();
        }

        [HttpDelete("{id}/remover-tarefa")]
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
        public async Task<ActionResult> AtualizarPrioridade(int id, [FromBody] AtualizarPrioridadeTarefaDto dto)
        {
            await aplic.AtualizarPrioridade(id, dto);
            return NoContent();
        }
    }
}
