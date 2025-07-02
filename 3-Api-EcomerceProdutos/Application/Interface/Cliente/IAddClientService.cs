using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Cliente;

public interface IAddClientService
{
    public Task<Result<CreateClientResponse>> CreateClientAsync (CreateClientRequest request);
}