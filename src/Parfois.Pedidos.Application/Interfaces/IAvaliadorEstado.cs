using Parfois.Pedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Interfaces
{
    public interface IAvaliadorEstado
    {
        List<string> Evaluate(Aprovacao aprovacao);
    }
}
