using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.DTOs
{
    public class ItemDto
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public int Qtd { get; set; }
    }
}
