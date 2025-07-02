using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Application.Validators;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Cliente;

public class UpdateClienteService : IUpdateClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public UpdateClienteService(IClienteRepository clienteRepository)
    {
         _clienteRepository = clienteRepository;
    }

    public async Task<Result> UpdateCliente(UpdateClientRequest request)
    {
        try
        { 
            var clientid = await _clienteRepository.GetClienteById(request.Id);
            var encriptedPassword =  PasswordEncrypt.Encriptor(request.Senha);
            
            clientid.UpdateCliente(request.Nome, request.Email, encriptedPassword);
            await _clienteRepository.SaveChangesAsync();
            
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}