using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Produto;

public interface IGetProductByIdService
{
    public Task<Result<ProdutoNew>>  GetProductById(Guid id);
}