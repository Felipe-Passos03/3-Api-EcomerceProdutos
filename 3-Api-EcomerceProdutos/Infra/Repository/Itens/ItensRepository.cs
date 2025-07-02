using _3_Api_EcomerceProdutos.Domain.Enums;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Itens;

public class ItensRepository :  IItensRepository
{
    private readonly AppDbContext _context;

    public ItensRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ItensPedidoNew?> GetItensByIdPedido(Guid produtoId)
    {
        return await 
            (from item in _context.ItensPedido
            join pedido in _context.Pedidos
                on item.PedidoId equals pedido.PedidoId
            where item.ProdutoId == produtoId && pedido.Status == StatusPedido.EmProcessamento
            select item).FirstOrDefaultAsync();
    }
}
