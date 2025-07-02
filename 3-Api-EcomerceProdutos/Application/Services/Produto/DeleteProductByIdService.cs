using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Infra.Repository.Itens;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class DeleteProductByIdService :  IDeleteProductByIdService
{
    private readonly IProdutoRepository  _produtoRepository;
    private readonly IItensRepository _itensRepository;

    public DeleteProductByIdService(IProdutoRepository produtoRepository,  
                                    IItensRepository itensRepository)
    {
        _produtoRepository = produtoRepository;
        _itensRepository = itensRepository;
    }

    public async Task<Result> DeleteProductById(Guid id)
    {
        try
        {
            var produtoEmPedido = _itensRepository.GetItensByIdPedido(id);
            if (produtoEmPedido != null)
                return Result.Fail("Esse produto não pode ser deletado.");
            
            var produtoExists = await _produtoRepository.GetProductByIdAsync(id);
            if(produtoExists == null)
                return Result.Fail("Esse produto não existe ");
        
            await _produtoRepository.DeleteProductAsync(id);
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}