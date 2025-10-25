using Application.Dtos.Filtros.FiltroToDoItemDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Services.ServToDoItems;
using Domain.ToDoItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ToDoItems
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

        [HttpGet]
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

        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] ToDoItemDto todoItemDto)
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] ToDoItemDto toDoItemDto)
        {

            await aplic.Atualizar(id, toDoItemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await aplic.Remover(id);
            return NoContent();
        }
    }
}
