using Microsoft.AspNetCore.Mvc;
using Parfois.Pedidos.Application.DTOs;
using Parfois.Pedidos.Application.Interfaces;

namespace Parfois.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Lista todos os pedidos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Obtém um pedido pelo código.
        /// </summary>
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var pedido = await _pedidoService.GetByCodigoAsync(codigo);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(PedidoDto pedidoDto)
        {
            try
            {
                var criado = await _pedidoService.CreateAsync(pedidoDto);

                if (!criado)
                    return Conflict($"Já existe um pedido com o código {pedidoDto.Pedido}.");

                return CreatedAtAction(nameof(GetByCodigo), new { codigo = pedidoDto.Pedido }, pedidoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os itens de um pedido existente.
        /// </summary>
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(string codigo, PedidoDto pedidoDto)
        {
            try
            {
                var atualizado = await _pedidoService.UpdateAsync(codigo, pedidoDto);

                if (!atualizado)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Apaga um pedido.
        /// </summary>
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var apagado = await _pedidoService.DeleteAsync(codigo);

            if (!apagado)
                return NotFound();

            return NoContent();
        }
    }
}