using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Produto;

public interface IUpdateProductService
{
    public Task<Result> UpdateProduct(UpdateProductRequest request);
}