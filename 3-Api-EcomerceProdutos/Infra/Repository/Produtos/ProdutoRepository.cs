using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using _3_Api_EcomerceProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Produtos;

public class ProdutoRepository : IProdutoRepository
{
    
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProdutoNew>> GetAllProductsAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async  Task<ProdutoNew> GetProductByIdAsync(Guid id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task AddProductAsync(ProdutoNew produtoNew)
    {
        await _context.Produtos.AddAsync(produtoNew);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
         _context.Produtos.Remove(await _context.Produtos.FindAsync(id));
    }

    public async Task<ProdutoNew> VerifyIfProductExistByName(string nome)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.Nome.ToLower() == nome.ToLower());
    }
}