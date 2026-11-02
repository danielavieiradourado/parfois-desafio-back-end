using Microsoft.EntityFrameworkCore;
using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Infrastructure
{
    public class PedidosDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; } = null!;

        public PedidosDbContext(DbContextOptions<PedidosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(pedido =>
            {
                pedido.HasKey(p => p.Codigo);

                pedido.OwnsMany(p => p.Itens);

                pedido.Navigation(p => p.Itens)
                      .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
