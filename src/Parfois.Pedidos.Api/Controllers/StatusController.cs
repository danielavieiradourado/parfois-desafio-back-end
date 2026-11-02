using Microsoft.AspNetCore.Mvc;
using Parfois.Pedidos.Application.DTOs;
using Parfois.Pedidos.Application.Interfaces;

namespace Parfois.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly IAprovacaoService _aprovacaoService;

        public StatusController(IAprovacaoService aprovacaoService)
        {
            _aprovacaoService = aprovacaoService;
        }

        /// <summary>
        /// Avalia a mudança de estado de um pedido e devolve os estados resultantes.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Evaluate(AprovacaoDto aprovacaoDto)
        {
            var resultado = await _aprovacaoService.EvaluateAsync(aprovacaoDto);
            return Ok(resultado);
        }
    }
}