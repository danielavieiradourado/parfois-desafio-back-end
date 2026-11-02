using Parfois.Pedidos.Application.DTOs;
using Parfois.Pedidos.Application.Interfaces;
using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Servicos
{
    public class AprovacaoService : IAprovacaoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IAvaliadorEstado _avaliador;

        public AprovacaoService(IPedidoRepository repository, IAvaliadorEstado avaliador)
        {
            _repository = repository;
            _avaliador = avaliador;
        }

        public async Task<ResultadoAprovacaoDto> EvaluateAsync(AprovacaoDto aprovacaoDto)
        {
            var pedido = await _repository.GetByCodigoAsync(aprovacaoDto.Pedido);

            var aprovacao = new Aprovacao(
                aprovacaoDto.Status,
                aprovacaoDto.ItensAprovados,
                aprovacaoDto.ValorAprovado,
                pedido);

            var estados = _avaliador.Evaluate(aprovacao);

            return new ResultadoAprovacaoDto
            {
                Pedido = aprovacaoDto.Pedido,
                Status = estados
            };
        }
    }
}