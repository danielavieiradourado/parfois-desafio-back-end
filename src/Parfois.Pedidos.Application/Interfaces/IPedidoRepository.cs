using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IReadOnlyList<Pedido>> GetAllAsync();
        Task<Pedido?> GetByCodigoAsync(string codigo);
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task RemoveAsync(Pedido pedido);
    }
}
