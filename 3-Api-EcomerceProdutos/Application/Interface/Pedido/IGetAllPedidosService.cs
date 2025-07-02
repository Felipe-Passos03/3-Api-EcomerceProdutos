using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Pedido;

public interface IGetAllPedidosService
{
    public Task<Result<List<PedidosNew>>> GetAllPedidos();
}