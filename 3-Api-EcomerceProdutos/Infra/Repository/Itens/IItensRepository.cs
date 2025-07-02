using _3_Api_EcomerceProdutos.Domain.Model.Itens;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Itens;

public interface IItensRepository
{
    public Task<ItensPedidoNew?> GetItensByIdPedido(Guid produtoId);
}