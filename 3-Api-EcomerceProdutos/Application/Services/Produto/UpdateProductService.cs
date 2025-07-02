using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class UpdateProductService : IUpdateProductService
{
    private readonly IProdutoRepository _produtoRepository;

    public UpdateProductService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    
    public async Task<Result> UpdateProduct(UpdateProductRequest request)
    {
        try
        {
            var productExist = await _produtoRepository.GetProductByIdAsync(request.Id);
           
            if(productExist == null)
                return Result.Fail("Esse ID é de um produto que não existe");
            
            productExist.AtualizarProduto(request.Nome, request.PrecoUnitario, request.Estoque);
            await _produtoRepository.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}