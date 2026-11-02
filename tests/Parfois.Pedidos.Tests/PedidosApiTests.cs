using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Parfois.Pedidos.Application.DTOs;

namespace Parfois.Pedidos.Tests
{
    public class PedidosApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PedidosApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        // Pedido do enunciado (20 € e 3 unidades) com um código único por teste
        private static PedidoDto CreatePedidoDto()
        {
            return new PedidoDto
            {
                Pedido = Guid.NewGuid().ToString(),
                Itens = new List<ItemDto>
                {
                    new ItemDto { Descricao = "Item A", PrecoUnitario = 10, Qtd = 1 },
                    new ItemDto { Descricao = "Item B", PrecoUnitario = 5, Qtd = 2 }
                }
            };
        }

        [Fact]
        public async Task Post_CreatesPedido_AndGetReturnsIt()
        {
            var pedido = CreatePedidoDto();

            var post = await _client.PostAsJsonAsync("/api/pedido", pedido);
            var get = await _client.GetFromJsonAsync<PedidoDto>($"/api/pedido/{pedido.Pedido}");

            Assert.Equal(HttpStatusCode.Created, post.StatusCode);
            Assert.NotNull(get);
            Assert.Equal(2, get.Itens.Count);
        }

        [Fact]
        public async Task Post_ReturnsConflict_WhenPedidoAlreadyExists()
        {
            var pedido = CreatePedidoDto();
            await _client.PostAsJsonAsync("/api/pedido", pedido);

            var response = await _client.PostAsJsonAsync("/api/pedido", pedido);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenPedidoHasNoItens()
        {
            var pedido = new PedidoDto { Pedido = Guid.NewGuid().ToString() };

            var response = await _client.PostAsJsonAsync("/api/pedido", pedido);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdatesItens()
        {
            var pedido = CreatePedidoDto();
            await _client.PostAsJsonAsync("/api/pedido", pedido);

            pedido.Itens = new List<ItemDto>
            {
                new ItemDto { Descricao = "Item C", PrecoUnitario = 7, Qtd = 4 }
            };
            var put = await _client.PutAsJsonAsync($"/api/pedido/{pedido.Pedido}", pedido);
            var get = await _client.GetFromJsonAsync<PedidoDto>($"/api/pedido/{pedido.Pedido}");

            Assert.Equal(HttpStatusCode.NoContent, put.StatusCode);
            Assert.NotNull(get);
            Assert.Single(get.Itens);
            Assert.Equal("Item C", get.Itens[0].Descricao);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenPedidoDoesNotExist()
        {
            var pedido = CreatePedidoDto();

            var response = await _client.PutAsJsonAsync($"/api/pedido/{pedido.Pedido}", pedido);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_RemovesPedido()
        {
            var pedido = CreatePedidoDto();
            await _client.PostAsJsonAsync("/api/pedido", pedido);

            var delete = await _client.DeleteAsync($"/api/pedido/{pedido.Pedido}");
            var get = await _client.GetAsync($"/api/pedido/{pedido.Pedido}");

            Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
        }

        [Theory]
        [InlineData("APROVADO", 3, 20, new[] { "APROVADO" })]
        [InlineData("APROVADO", 3, 10, new[] { "APROVADO_VALOR_A_MENOR" })]
        [InlineData("APROVADO", 4, 21, new[] { "APROVADO_VALOR_A_MAIOR", "APROVADO_QTD_A_MAIOR" })]
        [InlineData("APROVADO", 2, 20, new[] { "APROVADO_QTD_A_MENOR" })]
        [InlineData("REPROVADO", 0, 0, new[] { "REPROVADO" })]
        public async Task PostStatus_ReturnsExpectedEstados(string status, int itensAprovados, int valorAprovado, string[] esperado)
        {
            var pedido = CreatePedidoDto();
            await _client.PostAsJsonAsync("/api/pedido", pedido);

            var aprovacao = new AprovacaoDto
            {
                Status = status,
                ItensAprovados = itensAprovados,
                ValorAprovado = valorAprovado,
                Pedido = pedido.Pedido
            };
            var response = await _client.PostAsJsonAsync("/api/status", aprovacao);
            var resultado = await response.Content.ReadFromJsonAsync<ResultadoAprovacaoDto>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultado);
            Assert.Equal(pedido.Pedido, resultado.Pedido);
            Assert.Equal(esperado, resultado.Status);
        }

        [Fact]
        public async Task PostStatus_ReturnsCodigoPedidoInvalido_WhenPedidoDoesNotExist()
        {
            var aprovacao = new AprovacaoDto
            {
                Status = "APROVADO",
                ItensAprovados = 3,
                ValorAprovado = 20,
                Pedido = "123456-N"
            };

            var response = await _client.PostAsJsonAsync("/api/status", aprovacao);
            var resultado = await response.Content.ReadFromJsonAsync<ResultadoAprovacaoDto>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultado);
            Assert.Equal(new List<string> { "CODIGO_PEDIDO_INVALIDO" }, resultado.Status);
        }
    }
}
