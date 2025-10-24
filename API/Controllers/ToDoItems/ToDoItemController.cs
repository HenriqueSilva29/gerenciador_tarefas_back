using Application.Dtos;
using Application.Interfaces.IToDoItems;
using Application.Services.ToDoItemServices;
using Domain.ToDoItem;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers.ToDoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ServToDoItem aplic;

        public ToDoItemController(ServToDoItem toDoItemService)
        {
            aplic = toDoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> RecuperarTodos()
        {
            var toDoItems = await aplic.RecuperarTodos();
            return Ok(toDoItems);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Filtrar(Expression<Func<ToDoItem, bool>> parametros)
        {
            var filteredItems = await aplic.Filtrar(parametros);
            return Ok(filteredItems);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] ToDoItemDto todoItemDto)
        {
            if (todoItemDto == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            try
            {
                await aplic.Adicionar(todoItemDto);
                //return CreatedAtAction(nameof(RecuperarTodos), new { id = todoItem.idToDoItem }, todoItem);
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
