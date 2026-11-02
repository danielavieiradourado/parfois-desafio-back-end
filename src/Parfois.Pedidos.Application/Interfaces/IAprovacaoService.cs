using Parfois.Pedidos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parfois.Pedidos.Application.Interfaces
{
    public interface IAprovacaoService
    {
        Task<ResultadoAprovacaoDto> EvaluateAsync(AprovacaoDto aprovacaoDto);
    }
}
