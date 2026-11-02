using Parfois.Pedidos.Application.Servicos;
using Parfois.Pedidos.Domain.Constantes;
using Parfois.Pedidos.Domain.Entidades;

namespace Parfois.Pedidos.Tests
{
    public class AvaliadorEstadoTests
    {
        private readonly AvaliadorEstado _avaliador = new AvaliadorEstado();

        private static Pedido CreatePedido()
        {
            var itens = new List<Item>
            {
                new Item("Item A", 10, 1),
                new Item("Item B", 5, 2)
            };

            return new Pedido("123456", itens);
        }

        [Fact]
        public void Evaluate_ReturnsAprovado_WhenValorAndQtdMatch()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Aprovado, 3, 20, CreatePedido());

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.Aprovado }, estados);
        }

        [Fact]
        public void Evaluate_ReturnsValorAMenor_WhenValorIsLower()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Aprovado, 3, 10, CreatePedido());

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.AprovadoValorAMenor }, estados);
        }

        [Fact]
        public void Evaluate_ReturnsValorAMaiorAndQtdAMaior_WhenBothAreHigher()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Aprovado, 4, 21, CreatePedido());

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.AprovadoValorAMaior, EstadoPedido.AprovadoQtdAMaior }, estados);
        }

        [Fact]
        public void Evaluate_ReturnsQtdAMenor_WhenQtdIsLower()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Aprovado, 2, 20, CreatePedido());

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.AprovadoQtdAMenor }, estados);
        }

        [Fact]
        public void Evaluate_ReturnsReprovado_WhenEstadoIsReprovado()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Reprovado, 0, 0, CreatePedido());

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.Reprovado }, estados);
        }

        [Fact]
        public void Evaluate_ReturnsCodigoPedidoInvalido_WhenPedidoDoesNotExist()
        {
            var aprovacao = new Aprovacao(EstadoPedido.Aprovado, 3, 20, null);

            var estados = _avaliador.Evaluate(aprovacao);

            Assert.Equal(new List<string> { EstadoPedido.CodigoPedidoInvalido }, estados);
        }
    }
}
