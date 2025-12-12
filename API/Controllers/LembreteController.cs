using Application.Dtos.LembreteDtos;
using Application.Services.ServLembretes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LembreteController : ControllerBase
    {
        private readonly IServLembrete _serv;

        public LembreteController(IServLembrete serv)
        {
            _serv = serv;
        }

        [HttpPost("{tarefaId}")]
        public async Task<IActionResult> Criar(int tarefaId, CriarLembreteDto dto)
        {
            var lembrete = await _serv.CriarLembrete(tarefaId, dto.Date, dto.Texto);
            return Ok(lembrete);
        }
    }
}
