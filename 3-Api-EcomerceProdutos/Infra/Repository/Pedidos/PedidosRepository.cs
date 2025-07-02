using _3_Api_EcomerceProdutos.Domain.Enums;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;

public class PedidosRepository : IPedidosRepository
{
    private readonly AppDbContext _context;

    public PedidosRepository(AppDbContext context)
    {
        _context = context;
    }

    public async  Task AddPedido(PedidosNew? pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
    }

    public async Task DeletePedidoById(Guid pedidoId)
    {
        _context.Pedidos.Remove(await _context.Pedidos.FindAsync(pedidoId));
    }

    public async Task<PedidosNew?> GetPedidoById(Guid pedidoId)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(pedidoId => pedidoId == pedidoId);
    }

    public async Task<List<PedidosNew?>> GetAllPedidos()
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync(); //Usar para atualizar itens
    }

    public async Task<PedidosNew?> GetPedidosEmProcessamento(Guid pedidoId)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Status == StatusPedido.EmProcessamento && p.PedidoId == pedidoId);;
    }

    public async Task<PedidosNew> GetPedidosFinalizados(Guid pedidoId)
    {
        return await _context.Pedidos
            .Where(p => p.Status == StatusPedido.PedidoFinalizado && p.PedidoId == pedidoId)
            .FirstOrDefaultAsync();;
    }

    public async Task<PedidosNew?> GetPedidosEmProcessamentoByClientId(Guid clientId)
    {
        return await _context.Pedidos
            .Where(p => p.Status == StatusPedido.EmProcessamento && p.ClientId == clientId)
            .FirstOrDefaultAsync();;
    }

    public async Task<PedidosNew> GetPedidosFinalizadosByClientId(Guid clientId)
    {
        return await _context.Pedidos
            .Where(p => p.Status == StatusPedido.PedidoFinalizado && p.ClientId == clientId)
            .FirstOrDefaultAsync();;
    }

    public async Task RemoveItensDoPedido(Guid itensPedidoId)
    {
        var item = await _context.ItensPedido.FindAsync(itensPedidoId);
        if (item != null)
        {
            _context.ItensPedido.Remove(item);
        }
    }

    public async Task AddItensAoPedido(ItensPedidoNew itensPedido)
    {
        await _context.ItensPedido.AddAsync(itensPedido);
    }
}