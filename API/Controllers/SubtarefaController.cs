using Application.Dtos.SubtarefaDto;
using Application.Services.ServSubTarefas;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtarefaController : ControllerBase
    {
        private readonly IServSubtarefa aplic;

        public SubtarefaController(IServSubtarefa servSubtarefa)
        {
            aplic = servSubtarefa;
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult> AdicionarSubtarefa([FromBody] AdicionarSubtarefaDto dto)
        {
            try
            {
                await aplic.AdicionarSubtarefa(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
