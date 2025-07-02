using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Cliente;

public class GetClientesByIdService : IGetClientesByIdService
{
    private readonly IClienteRepository _clienteRepository;

    public GetClientesByIdService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
    public async Task<Result<ClienteNew>> GetClientesByIDAsync(Guid id)
    {
        try
        {
            var clientes = await _clienteRepository.GetClienteById(id);
            if (clientes == null)
                return Result.Fail("Esse cliente não existe");
            
            return Result.Ok(clientes);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}