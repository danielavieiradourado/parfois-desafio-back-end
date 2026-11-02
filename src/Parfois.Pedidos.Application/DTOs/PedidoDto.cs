using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.DTOs
{
    public class PedidoDto
    {
        public string Pedido { get; set; } = string.Empty;
        public List<ItemDto> Itens { get; set; } = new();
    }
}
