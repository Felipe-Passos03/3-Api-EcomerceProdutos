using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class GetAllProductsService : IGetAllProductsService
{
    private readonly IProdutoRepository _produtoRepository;

    public GetAllProductsService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task<Result<List<ProdutoNew>>> GetAllProducts()
    {
        try
        {
            var result = await _produtoRepository.GetAllProductsAsync();
           
            if (result == null)
                return Result.Fail("A lista de produtos está vazia");
        
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ProdutoNew>>(ex.Message);
        }
    }
}