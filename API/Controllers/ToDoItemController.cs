using API.Errors;
using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Services.ServToDoItems;
using Application.Utils.Paginacao;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IServToDoItem aplic;

        public ToDoItemController(IServToDoItem servToDoItem)
        {
            aplic = servToDoItem;
        }

        [HttpGet("listar-tarefas")]
        public async Task<ActionResult<PaginacaoHelper<ToDoItem>>> Listar([FromQuery] FiltroToDoItemDto filtroToDoItemDto)
        {
            var filteredItems = await aplic.ListarTarefas(filtroToDoItemDto);
            return Ok(filteredItems);
        }

        [HttpPost("criar-tarefa")]
        public async Task<ActionResult> CriarTarefa([FromBody] AdicionarToDoItemDto todoItemDto)
        {
                await aplic.AdicionarTarefa(todoItemDto);
                return Ok();  
        }

        [HttpPut("{id}/atualizar-tarefa")]
        public async Task<ActionResult> Atualizar([FromQuery] int id, [FromBody] AtualizarToDoItemDto toDoItemDto)
        {
            await aplic.AtualizarTarefa(id, toDoItemDto);
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
        public async Task<ActionResult> AtualizarPrioridade(int id, [FromBody] AtualizarPrioridadeToDoItemDto dto)
        {
            await aplic.AtualizarPrioridade(id, dto);
            return NoContent();
        }
    }
}
