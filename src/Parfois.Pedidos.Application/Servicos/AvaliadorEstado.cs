using Parfois.Pedidos.Application.Interfaces;
using Parfois.Pedidos.Domain.Constantes;
using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Servicos
{
    public class AvaliadorEstado : IAvaliadorEstado
    {
        public List<string> Evaluate(Aprovacao aprovacao)
        {
            var estados = new List<string>();

            if (aprovacao.Pedido == null)
            {
                estados.Add(EstadoPedido.CodigoPedidoInvalido);
                return estados;
            }

            if (aprovacao.Estado == EstadoPedido.Reprovado)
            {
                estados.Add(EstadoPedido.Reprovado);
                return estados;
            }

            if (aprovacao.Estado != EstadoPedido.Aprovado)
                return estados;

            decimal valorTotal = aprovacao.Pedido.ValorTotal;
            int quantidadeTotal = aprovacao.Pedido.QuantidadeTotal;

            if (aprovacao.ValorAprovado == valorTotal && aprovacao.ItensAprovados == quantidadeTotal)
                estados.Add(EstadoPedido.Aprovado);

            if (aprovacao.ValorAprovado < valorTotal)
                estados.Add(EstadoPedido.AprovadoValorAMenor);

            if (aprovacao.ValorAprovado > valorTotal)
                estados.Add(EstadoPedido.AprovadoValorAMaior);

            if (aprovacao.ItensAprovados < quantidadeTotal)
                estados.Add(EstadoPedido.AprovadoQtdAMenor);

            if (aprovacao.ItensAprovados > quantidadeTotal)
                estados.Add(EstadoPedido.AprovadoQtdAMaior);

            return estados;
        }
    }

}