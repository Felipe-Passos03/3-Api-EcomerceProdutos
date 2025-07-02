using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using _3_Api_EcomerceProdutos.Infra.Data;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class GetProductByIdService :  IGetProductByIdService
{
    private readonly IProdutoRepository _produtoRepository;

    public GetProductByIdService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task<Result<ProdutoNew>> GetProductById(Guid id)
    {
        try
        {
            var productExists = await _produtoRepository.GetProductByIdAsync(id);
            if (productExists == null)
                return Result.Fail("Esse produto não existe");
        
            return Result.Ok(productExists);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}