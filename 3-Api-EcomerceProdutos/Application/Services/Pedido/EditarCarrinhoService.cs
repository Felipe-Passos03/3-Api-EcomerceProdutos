using _3_Api_EcomerceProdutos.Application.DTOs.Item;
using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Application.Services.Pedido;

public class EditarCarrinhoService : IEditarCarrinhoService
{
    private readonly IPedidosRepository _pedidosRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IClienteRepository _clienteRepository;

    public EditarCarrinhoService(IPedidosRepository pedidosRepository,
        IProdutoRepository produtoRepository,
        IClienteRepository clienteRepository)
    {
        _pedidosRepository = pedidosRepository;
        _produtoRepository = produtoRepository;
        _clienteRepository = clienteRepository;

    }

    public async Task<Result<PedidosNew>> EditarItensNoCarrinho(ItensRequest request)
    {
        try
        {
            var carrinho = await _pedidosRepository.GetPedidosEmProcessamento(request.PedidoId);
            if (carrinho == null)
                return Result.Fail("Esse pedido já foi finalizado, não é possível editá-lo");

            var itensParaEditar = request.EditarItens;

            // Cópia da lista original
            var itensExistentes = carrinho.Itens.ToList();

            // Produtos que não estão mais na requisição
            var produtosParaRemover = itensExistentes
                .Where(i => !itensParaEditar.Any(e => e.ProdutoId == i.ProdutoId))
                .ToList();

            foreach (var itemRemover in produtosParaRemover)
                         {
                             _pedidosRepository.RemoveItensDoPedido(itemRemover.ItensPedidoId);
                             carrinho.Itens.Remove(itemRemover);
                         }

            // Atualiza a lista de itens após as remoções
            itensExistentes = carrinho.Itens.ToList();

            // Atualizar ou adicionar os itens restantes
            foreach (var itemReq in itensParaEditar)
            {
                var produto = await _produtoRepository.GetProductByIdAsync(itemReq.ProdutoId);
                if (produto == null)
                    return Result.Fail($"Produto {itemReq.ProdutoId} não encontrado");

                if (produto.Estoque < itemReq.Quantidade)
                    return Result.Fail(
                        $"Produto {produto.Nome} não possui estoque suficiente. Estoque atual: {produto.Estoque}");

                var itemExistente = itensExistentes.FirstOrDefault(i => i.ProdutoId == itemReq.ProdutoId);

                if (itemExistente != null)
                {
                    itemExistente.AtualizarItens(itemReq.Quantidade, produto.PrecoUnitario);
                }
                else
                {
                    var novoItem = new ItensPedidoNew(produto.ProdutoId, itemReq.Quantidade, produto.PrecoUnitario)
                    {
                        PedidoId = carrinho.PedidoId
                    };
                    
                    await _pedidosRepository.AddItensAoPedido(novoItem);
                    
                }
            }


            await _pedidosRepository.SaveChangesAsync();

            return Result.Ok(carrinho);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}