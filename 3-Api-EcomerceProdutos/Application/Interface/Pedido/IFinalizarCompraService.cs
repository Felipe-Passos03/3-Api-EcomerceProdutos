using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Pedido;

public interface IFinalizarCompraService
{
    public Task<Result> FinalizarCompra(Guid pedidoId); 
}