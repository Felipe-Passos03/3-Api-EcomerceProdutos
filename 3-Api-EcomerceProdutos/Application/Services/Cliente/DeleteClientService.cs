using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Cliente;

public class DeleteClientService : IDeleteClientService
{
    private readonly IClienteRepository _clienteRepository;

    public DeleteClientService(IClienteRepository clienteRepository)
    {
       _clienteRepository = clienteRepository;
    }
    
    public async Task<Result> DeleteClienteById(Guid clienteId)
    {
        try
        {
            var clientExiste = await _clienteRepository.GetClienteById(clienteId);
            if (clientExiste == null)
                return Result.Fail("Esse cliente não existe");

            await _clienteRepository.DeleteClienteById(clienteId);
            await _clienteRepository.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}