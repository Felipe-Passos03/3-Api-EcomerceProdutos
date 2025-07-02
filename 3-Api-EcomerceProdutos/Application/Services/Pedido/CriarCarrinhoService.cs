using _3_Api_EcomerceProdutos.Application.DTOs.Item;
using _3_Api_EcomerceProdutos.Application.DTOs.Pedido;
using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class CriarCarrinhoService :  ICriarCarrinhoService
{
    private readonly IPedidosRepository  _pedidosRepository;
    private readonly IProdutoRepository  _produtoRepository;
    private readonly IClienteRepository  _clienteRepository;

    public CriarCarrinhoService(IPedidosRepository pedidosRepository,  
                            IProdutoRepository produtoRepository, 
                            IClienteRepository clienteRepository)
    {
        _pedidosRepository = pedidosRepository;
        _produtoRepository = produtoRepository;
        _clienteRepository = clienteRepository;
    }
    
    public async Task<Result> AddPedidoAoCarrinhoAsync(AddPedidoRequest request)
    {
        try
        {
            
            var clienteExiste = await _clienteRepository.GetClienteById(request.ClientId);
                if(clienteExiste == null)
                    return Result.Fail("Esse cliente não existe");
            
            if(request.Itens == null || request.Itens.Count == 0)
                return Result.Fail("Não é possível criar um carrinho vazio.");
            
            
            var carrinhoAtivo =
                await _pedidosRepository.GetPedidosEmProcessamentoByClientId(request.ClientId);
            
            if(carrinhoAtivo != null)
                return Result.Fail("Você já tem um carrinho ativo de pedidos," +
                                   " não é possível criar outro, finalize a compra ou delete o carrinho atual.");
            
            var pedido = new PedidosNew(request.ClientId, 0, 0);

            foreach (var item in request.Itens)
            {
                var produto = await _produtoRepository.GetProductByIdAsync(item.ProdutoId);
               
                if (produto == null)
                    return Result.Fail($"Produto {item.ProdutoId} não encontrado");
                
                if(produto.Estoque < item.Quantidade)
                    return Result.Fail($"Produto {produto.Nome} não possui estoque suficiente. O estoque atual é de {produto.Estoque}");

                var addItem = new ItensPedidoNew(item.ProdutoId, item.Quantidade, produto.PrecoUnitario);
                pedido.CriarCarrinhoComCalculoDesconto(addItem);

            }
                await _pedidosRepository.AddPedido(pedido);
                await _pedidosRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
        
        return Result.Ok();
    }
}