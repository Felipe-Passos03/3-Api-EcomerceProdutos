using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Application.Validators;
using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using FluentResults;

namespace _3_Api_EcomerceProdutos.Application.Services.Cliente;

public class AddClientService :  IAddClientService
{
    private readonly IClienteRepository _clienteRepository;

    public AddClientService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
    
    public async Task<Result<CreateClientResponse>> CreateClientAsync(CreateClientRequest request)
    {
        try
        {
            bool EmailExiste = await _clienteRepository.VerificaClientExistByEmail(request.Email);
            if(EmailExiste)
                return Result.Fail("Esse email já está cadastrado no banco de dados");
            
            bool CpfJaExiste = await _clienteRepository.VerificaClientExistByCPF(request.CPF);
            if(CpfJaExiste)
                return Result.Fail("Este CPF já está cadastrado no banco de dados");
            
            bool CpfVaLido = CpfValidator.IsValid(request.CPF);

            if (!CpfVaLido)
                return Result.Fail("O CPF não é válido");
            
           var encriptedPassword =  PasswordEncrypt.Encriptor(request.Senha);
            
            var addClient = new ClienteNew(request.Nome , request.Email , request.CPF, encriptedPassword);
            await _clienteRepository.CreateCliente(addClient);
            await _clienteRepository.SaveChangesAsync();
            
            var response = new CreateClientResponse
            {
                ClienteId = addClient.ClientId,
                Nome = addClient.Nome,
                Email = addClient.Email,
                CPF = addClient.CPF,
                Senha = addClient.Senha
            };
        
            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
    
    
}