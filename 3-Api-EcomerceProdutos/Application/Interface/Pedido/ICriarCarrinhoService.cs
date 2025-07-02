using _3_Api_EcomerceProdutos.Application.DTOs.Item;
using _3_Api_EcomerceProdutos.Application.DTOs.Pedido;
using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Pedido;

public interface ICriarCarrinhoService
{
    public Task<Result> AddPedidoAoCarrinhoAsync(AddPedidoRequest request);
}