using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Cliente;

public interface IUpdateClienteService
{
    public Task<Result> UpdateCliente(UpdateClientRequest request);
}