using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Domain.Entidades
{
    public class Item
    {
        public string Descricao { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Qtd { get; private set; }

        public decimal ValorTotal => PrecoUnitario * Qtd;

        public Item(string descricao, decimal precoUnitario, int qtd)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição do item é obrigatória.", nameof(descricao));

            if (precoUnitario < 0)
                throw new ArgumentException("O preço unitário não pode ser negativo.", nameof(precoUnitario));

            if (qtd <= 0)
                throw new ArgumentException("A quantidade tem de ser maior que zero.", nameof(qtd));

            Descricao = descricao;
            PrecoUnitario = precoUnitario;
            Qtd = qtd;
        }
    }
}
