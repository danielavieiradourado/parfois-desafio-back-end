using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Domain.Entidades
{
    public class Pedido
    {
        public string Codigo { get; private set; } = null!;
        private readonly List<Item> _itens = new();

        public IReadOnlyCollection<Item> Itens => _itens.AsReadOnly();

        public decimal ValorTotal => _itens.Sum(i => i.ValorTotal);
        public int QuantidadeTotal => _itens.Sum(i => i.Qtd);

        private Pedido() { }

        public Pedido(string codigo, IEnumerable<Item> itens)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("O código do pedido é obrigatório.", nameof(codigo));

            Codigo = codigo;
            UpdateItens(itens);
        }

        public void UpdateItens(IEnumerable<Item> itens)
        {
            var lista = itens?.ToList() ?? new List<Item>();

            if (lista.Count == 0)
                throw new ArgumentException("O pedido tem de ter pelo menos um item.", nameof(itens));

            _itens.Clear();
            _itens.AddRange(lista);
        }
    }
}
