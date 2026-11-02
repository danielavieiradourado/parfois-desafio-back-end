using Parfois.Pedidos.Application.DTOs;
using Parfois.Pedidos.Application.Interfaces;
using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Servicos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PedidoDto>> GetAllAsync()
        {
            var pedidos = await _repository.GetAllAsync();

            var resultado = new List<PedidoDto>();
            foreach (var pedido in pedidos)
            {
                resultado.Add(ToDto(pedido));
            }

            return resultado;
        }

        public async Task<PedidoDto?> GetByCodigoAsync(string codigo)
        {
            var pedido = await _repository.GetByCodigoAsync(codigo);

            if (pedido == null)
                return null;

            return ToDto(pedido);
        }

        public async Task<bool> CreateAsync(PedidoDto pedidoDto)
        {
            var existente = await _repository.GetByCodigoAsync(pedidoDto.Pedido);

            if (existente != null)
                return false;

            var pedido = new Pedido(pedidoDto.Pedido, ToItens(pedidoDto.Itens));
            await _repository.AddAsync(pedido);

            return true;
        }

        public async Task<bool> UpdateAsync(string codigo, PedidoDto pedidoDto)
        {
            var pedido = await _repository.GetByCodigoAsync(codigo);

            if (pedido == null)
                return false;

            pedido.UpdateItens(ToItens(pedidoDto.Itens));
            await _repository.UpdateAsync(pedido);

            return true;
        }

        public async Task<bool> DeleteAsync(string codigo)
        {
            var pedido = await _repository.GetByCodigoAsync(codigo);

            if (pedido == null)
                return false;

            await _repository.RemoveAsync(pedido);

            return true;
        }

        private static PedidoDto ToDto(Pedido pedido)
        {
            var dto = new PedidoDto { Pedido = pedido.Codigo };

            foreach (var item in pedido.Itens)
            {
                dto.Itens.Add(new ItemDto
                {
                    Descricao = item.Descricao,
                    PrecoUnitario = item.PrecoUnitario,
                    Qtd = item.Qtd
                });
            }

            return dto;
        }

        private static List<Item> ToItens(List<ItemDto> itensDto)
        {
            var itens = new List<Item>();

            foreach (var itemDto in itensDto)
            {
                itens.Add(new Item(itemDto.Descricao, itemDto.PrecoUnitario, itemDto.Qtd));
            }

            return itens;
        }
    }
}