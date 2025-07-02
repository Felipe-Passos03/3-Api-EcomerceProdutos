using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Cliente;

public class GetAllClientesService : IGetAllClientesService
{
    private readonly IClienteRepository _clienteRepository;

    public GetAllClientesService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
    
    public async Task<Result<List<ClienteNew>>> GetAllClientesAsync()
    {
        try
        {
            var result = await _clienteRepository.GetAllClientes();
           
            if (result == null)
                return Result.Fail("A lista está vazia");
        
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ClienteNew>>(ex.Message);
        }
    }
}