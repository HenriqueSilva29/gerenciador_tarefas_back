using Application.Dtos.Filtros;
using Application.Dtos.ToDoItemDtos;
using Application.Services.ServToDoItems;
using Domain.ToDoItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

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

        [HttpGet("recuperar-todos")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> RecuperarTodos()
        {
            var toDoItems = await aplic.RecuperarTodos();
            return Ok(toDoItems);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Filtrar([FromQuery] FiltroToDoItemDto filtroToDoItemDto)
        {
            var filteredItems = await aplic.Filtrar(filtroToDoItemDto);
            return Ok(filteredItems);
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] AdicionarToDoItemDto todoItemDto)
        {
            try
            {
                await aplic.Adicionar(todoItemDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}/atualizar")]
        public async Task<ActionResult> Atualizar([FromQuery] int id, [FromBody] AtualizarToDoItemDto toDoItemDto)
        {

            await aplic.Atualizar(id, toDoItemDto);
            return NoContent();
        }

        [HttpDelete("{id}/remover")]
        public async Task<ActionResult> Remover(int id)
        {
            await aplic.Remover(id);
            return NoContent();
        }

        [HttpGet("tarefas-vencidas")]
        public async Task<ActionResult> RecuperarTarefasVencidas()
        {
            try
            {
                var tarefas = await aplic.RecuperarTarefasVencidas();
                return Ok(tarefas);
            }
            catch(Exception e)
            {
                return StatusCode(500,e.Message) ;
            }

        }


        [HttpPost("{id}/atualizar-prioridade")]
        public async Task<ActionResult> AtualizarPrioridade(int id, [FromBody] AtualizarPrioridadeDto dto)
        {
            await aplic.AtualizarPrioridade(id, dto);
            return NoContent();
        }
    }
}
