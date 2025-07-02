using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Interface.Cliente;

public interface IGetAllClientesService
{
    public Task<Result<List<ClienteNew>>> GetAllClientesAsync();
}