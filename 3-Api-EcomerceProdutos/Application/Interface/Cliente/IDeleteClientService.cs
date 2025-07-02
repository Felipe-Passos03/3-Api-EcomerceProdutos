using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Cliente;

public interface IDeleteClientService
{
    public Task<Result> DeleteClienteById(Guid clienteId);
}