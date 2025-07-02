using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class DeletePedidoByIdService :  IDeletePedidoByIdService
{
    private readonly IPedidosRepository  _pedidosRepository;

    public DeletePedidoByIdService(IPedidosRepository  pedidosRepository)
    {
        _pedidosRepository = pedidosRepository;
    }
    public async Task<Result> DeletePedidoById(Guid pedidoId)
    {
        try
        {
            var ExistePedido = await _pedidosRepository.GetPedidoById(pedidoId);
            if (ExistePedido == null)
                return Result.Fail("Esse pedido não existe");

            var pedidoEmProcessamento = await _pedidosRepository.GetPedidosEmProcessamento(pedidoId);
            if (pedidoEmProcessamento == null)
                return Result.Fail("Esse pedido já foi efetuado não é possível deleta-lo");
            
            await _pedidosRepository.DeletePedidoById(pedidoId);
            await _pedidosRepository.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
           return Result.Fail(e.Message);
        }
    }
}