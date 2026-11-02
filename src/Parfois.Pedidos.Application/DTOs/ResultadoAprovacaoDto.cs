using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.DTOs
{
    public class ResultadoAprovacaoDto
    {
        public string Pedido { get; set; } = string.Empty;
        public List<string> Status { get; set; } = new();
    }
}
