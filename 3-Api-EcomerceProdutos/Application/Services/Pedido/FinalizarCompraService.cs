using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class FinalizarCompraService :  IFinalizarCompraService
{
    private readonly IPedidosRepository  _pedidosRepository;
    private readonly IProdutoRepository  _produtoRepository;

    public FinalizarCompraService(IPedidosRepository pedidosRepository, 
                                  IProdutoRepository produtoRepository)
    {
        _pedidosRepository = pedidosRepository; 
        _produtoRepository = produtoRepository;
    }
    public async Task<Result> FinalizarCompra(Guid pedidoId)
    {
        try
        {
            var getPedido = await _pedidosRepository.GetPedidosEmProcessamento(pedidoId);
            if (getPedido == null)
                return Result.Fail("Esse pedido já foi finalizado ou não existe");

            foreach (var item in getPedido.Itens)
            {
             var produto = await _produtoRepository.GetProductByIdAsync(item.ProdutoId);
             produto.DescontarEstoque(item.Quantidade);
            }
            
            var subtotal = getPedido.Itens.Sum(i => i.Subtotal);
            getPedido.CalcularDesconto(subtotal);
            getPedido.FinalizarPedido(subtotal);
            
            await _pedidosRepository.SaveChangesAsync();
            await  _produtoRepository.SaveChangesAsync();
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}