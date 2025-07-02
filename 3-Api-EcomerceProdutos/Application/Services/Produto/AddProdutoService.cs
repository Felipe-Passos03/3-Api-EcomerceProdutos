using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class AddProdutoService : IAddProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public AddProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    
    public async Task<Result<AddProdutoResponse>> AddProduto(AddProdutoRequest request)
    {
        try
        {
            var ProdutoExiste = await _produtoRepository.VerifyIfProductExistByName(request.Nome);
            if (ProdutoExiste != null)
                return Result.Fail("Já existe um produto com esse nome registrado");
            
            ValidarQuantidadeEstoque.ValidarQtdEstoque(request.Estoque);

            var product = new Domain.Model.Produto.ProdutoNew(request.Nome, request.PrecoUnitario, request.Estoque);
            await _produtoRepository.AddProductAsync(product);
            await _produtoRepository.SaveChangesAsync();
        
            var response = new AddProdutoResponse
            {  ProdutoId = product.ProdutoId,
                Nome = product.Nome,
                PrecoUnitario = product.PrecoUnitario,
                Estoque = product.Estoque,
            };
            return Result.Ok(response);
        }
        catch (Exception e)
        {
            return Result.Fail<AddProdutoResponse>(e.Message);
        }
    }
}