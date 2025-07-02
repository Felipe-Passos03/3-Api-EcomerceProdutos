using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;

public interface IPedidosRepository
{
    public Task AddPedido(PedidosNew? pedido);
    public Task DeletePedidoById(Guid pedidoId);
    public Task<PedidosNew?> GetPedidoById(Guid pedidoId);
    public Task<List<PedidosNew?>> GetAllPedidos();
    public Task SaveChangesAsync();
    public Task<PedidosNew?> GetPedidosEmProcessamento(Guid pedidoId);
    public Task<PedidosNew> GetPedidosFinalizados(Guid pedidoId);
    public Task<PedidosNew?> GetPedidosEmProcessamentoByClientId(Guid clientId);
    public Task<PedidosNew> GetPedidosFinalizadosByClientId(Guid clientId);
    public Task RemoveItensDoPedido(Guid itensPedidoId);
    public Task AddItensAoPedido(ItensPedidoNew itensPedido);

}