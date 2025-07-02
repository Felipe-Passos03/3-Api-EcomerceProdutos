using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Produto;

public interface IAddProdutoService
{
    public Task<Result<AddProdutoResponse>> AddProduto(AddProdutoRequest request);
}

