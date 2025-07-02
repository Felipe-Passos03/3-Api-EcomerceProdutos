using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class GetAllPedidoService :  IGetAllPedidosService
{
    private readonly IPedidosRepository _pedidosRepository;

    public GetAllPedidoService(IPedidosRepository pedidosRepository)
    {
        _pedidosRepository = pedidosRepository;
    }
    public async Task<Result<List<PedidosNew>>> GetAllPedidos()
    {
        try
        {
            var getLista = await _pedidosRepository.GetAllPedidos();
            if (getLista == null)
                return Result.Fail("Lista de pedidos está vazia");
            
            return Result.Ok(getLista);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}