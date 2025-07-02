using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3_Api_EcomerceProdutos.Controller.Cliente;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class ClientController : ControllerBase
{
    private readonly IGetAllClientesService _getAllClientesService;
    private readonly IGetClientesByIdService _getClientesByIdService;
    private readonly IUpdateClienteService   _updateClienteService;
    private readonly IDeleteClientService    _deleteClientService;

    public ClientController(IAddClientService addClientService,  
                            IGetAllClientesService getAllClientesService,
                            IGetClientesByIdService getClientesByIdService,
                            IUpdateClienteService   updateClienteService,
                            IDeleteClientService   deleteClientService
                            )
    {
        _getAllClientesService = getAllClientesService;
        _getClientesByIdService = getClientesByIdService;
        _updateClienteService = updateClienteService;
        _deleteClientService = deleteClientService;
    }
    
    [HttpGet]
    [Route("getAllClients")]
    public async Task<IActionResult> GetAllClients()
    {
        var lista = await _getAllClientesService.GetAllClientesAsync();
            return Ok(lista.Value);
    }
    
    [HttpGet]
    [Route("getClientByID")]
    public async Task<IActionResult> GetClientesByID([FromQuery]Guid clientId)
    {
        if (clientId == Guid.Empty)
            return BadRequest("O campo id está vazio");
        
        var client = await _getClientesByIdService.GetClientesByIDAsync(clientId);
        if (client == null)
            return NotFound();
        
        return Ok(client.Value);
    }

    [HttpPost]
    [Route("atualizarCliente")]
    public async Task<IActionResult> AtualizarClienteAsync([FromBody] UpdateClientRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        var update = _updateClienteService.UpdateCliente(request);
        
        if (update.IsFaulted)
             return BadRequest("Falha ao atualizar cliente");
        
        return Ok("Cliente atualizado com sucesso");
    }

    [HttpDelete]
    [Route("deleteClient")]
    public async Task<IActionResult> DeleteClient([FromQuery]Guid clientId)
    {
        if (clientId == Guid.Empty)
            return BadRequest("O campo id está vazio");
        
        var delete = await _deleteClientService.DeleteClienteById(clientId);
        
        if(delete.IsFailed)
            return BadRequest(delete.Errors.First().Message);
        
        return Ok("Deletado com sucesso");
    }
}