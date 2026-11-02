using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Domain.Entidades
{
    public class Aprovacao
    {
        public string Estado { get; }
        public int ItensAprovados { get; }
        public decimal ValorAprovado { get; }
        public Pedido? Pedido { get; }

        public Aprovacao(string estado, int itensAprovados, decimal valorAprovado, Pedido? pedido)
        {
            Estado = estado;
            ItensAprovados = itensAprovados;
            ValorAprovado = valorAprovado;
            Pedido = pedido;
        }
    }
}
