using Parfois.Pedidos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<List<PedidoDto>> GetAllAsync();
        Task<PedidoDto?> GetByCodigoAsync(string codigo);
        Task<bool> CreateAsync(PedidoDto pedidoDto);
        Task<bool> UpdateAsync(string codigo, PedidoDto pedidoDto);
        Task<bool> DeleteAsync(string codigo);
    }
}
