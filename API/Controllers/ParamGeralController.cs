using Application.Funcionalidades.ParamGerais.Dtos;
using Application.Funcionalidades.ParamGerais.Servicos;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParamGeralController : ControllerBase
    {
        private readonly IServicoParamGeral _aplic;

        public ParamGeralController(IServicoParamGeral aplic)
        {
            _aplic = aplic;
        }

        [HttpGet]
        public async Task<ActionResult<ParamGeral>> Obter()
        {
            var result = await _aplic.Obter();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ParamGeral>> Atualizar([FromBody] AtualizarParamGeralRequisicao dto)
        {
            await _aplic.Atualizar(dto);
            return NoContent();
        }
    }
}



