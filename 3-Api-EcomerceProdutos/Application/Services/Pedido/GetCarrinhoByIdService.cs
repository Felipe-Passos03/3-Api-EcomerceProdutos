using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class GetCarrinhoByIdService : IGetCarrinhoByIdService
{
    private readonly IPedidosRepository  _pedidosRepository;

    public GetCarrinhoByIdService(IPedidosRepository pedidosRepository)
    {
        _pedidosRepository = pedidosRepository;
    }
    public async Task<Result<PedidosNew>> GetCarrinhoById(Guid pedidoId)
    {
        try
        {
            var pedido = await _pedidosRepository.GetPedidoById(pedidoId);
           
            if (pedido == null)
                return Result.Fail("Esse pedido não existe");

            return pedido;
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
        
    }
}