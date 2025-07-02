using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Pedido;

public interface IDeletePedidoByIdService
{
    public Task<Result> DeletePedidoById(Guid pedidoId);
}