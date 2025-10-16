using Application.Services.ToDoItemServices;
using Domain.ToDoItem;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ToDoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoItemService aplic;

        public ToDoItemController(ToDoItemService toDoItemService)
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
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Filtrar(string category, bool? isCompleted)
        {
            var filteredItems = await aplic.Filtrar(category, isCompleted);
            return Ok(filteredItems);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] ToDoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            try
            {
                await aplic.Adicionar(todoItem);  // O serviço que você implementou
                return CreatedAtAction(nameof(RecuperarTodos), new { id = todoItem.idToDoItem }, todoItem);
            }
            catch (Exception ex)
            {
                // Log de erro
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] ToDoItem todoItem)
        {
            if (id != todoItem.idToDoItem)
            {
                return BadRequest("Id mismatch.");
            }

            await aplic.Atualizar(todoItem);
            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await aplic.Remover(id);
            return NoContent();
        }
    }
}
