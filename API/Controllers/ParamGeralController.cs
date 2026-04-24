using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.ParamGerals;
using Application.Services.ServParamGerals;
using Application.Utils.Paginacao;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamGeralController : ControllerBase
    {
        private IServParamGeral _aplic;
        public ParamGeralController(IServParamGeral aplic)
        {
            _aplic = aplic;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<PaginacaoHelper<ParamGeral>>> Listar([FromQuery] ParamGeralFiltroRequest filtro)
        {
            var result = await _aplic.Listar(filtro);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaginacaoHelper<ParamGeral>>> Atualizar([FromRoute] int id, [FromBody] UpdateParamGeralRequest dto)
        {
            await _aplic.Atualizar(id, dto);

            return NoContent();
        }

    }
}
