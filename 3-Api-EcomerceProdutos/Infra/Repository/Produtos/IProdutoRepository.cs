using _3_Api_EcomerceProdutos.Domain.Model.Produto;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Produtos;

public interface IProdutoRepository
{
    public Task<List<ProdutoNew>> GetAllProductsAsync();
    public Task<ProdutoNew>  GetProductByIdAsync(Guid id);
    public Task AddProductAsync (ProdutoNew produtoNew);
    public Task SaveChangesAsync();
    public Task DeleteProductAsync(Guid id);
    public Task<ProdutoNew> VerifyIfProductExistByName(string nome);
}