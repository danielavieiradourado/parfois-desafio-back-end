using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Domain.Constantes
{
    public static class EstadoPedido
    {
        public const string Aprovado = "APROVADO";
        public const string Reprovado = "REPROVADO";
        public const string AprovadoValorAMenor = "APROVADO_VALOR_A_MENOR";
        public const string AprovadoValorAMaior = "APROVADO_VALOR_A_MAIOR";
        public const string AprovadoQtdAMenor = "APROVADO_QTD_A_MENOR";
        public const string AprovadoQtdAMaior = "APROVADO_QTD_A_MAIOR";
        public const string CodigoPedidoInvalido = "CODIGO_PEDIDO_INVALIDO";
    }
}
