using _3_Api_EcomerceProdutos.Application.DTOs.Item;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Pedido;

public interface IEditarCarrinhoService
{
    public Task<Result<PedidosNew>> EditarItensNoCarrinho(ItensRequest request);
}