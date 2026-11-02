using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.DTOs
{
    public class AprovacaoDto
    {
        public string Status { get; set; } = string.Empty;
        public int ItensAprovados { get; set; }
        public decimal ValorAprovado { get; set; }
        public string Pedido { get; set; } = string.Empty;
    }
}
