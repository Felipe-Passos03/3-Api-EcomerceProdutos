using _3_Api_EcomerceProdutos.Application.DTOs.Auth;
using _3_Api_EcomerceProdutos.Application.DTOs.Cliente;
using _3_Api_EcomerceProdutos.Application.Interface.Auth;
using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace _3_Api_EcomerceProdutos.Controller.Auth;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
 
    private readonly ILoginService _loginService;
    private readonly IAddClientService _addClientService;

    public AuthController(ILoginService loginService, 
                          IAddClientService addClientService)
    {
        _loginService = loginService;
        _addClientService = addClientService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        var result = await _loginService.LoginAsync(request);
        
        if(result == null)
            return Unauthorized("Email e senha inválidos");
        
        return Ok(result);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateClientAsync([FromBody]CreateClientRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        var result =  await _addClientService.CreateClientAsync(request);
        
        if (result.IsFailed)
            return BadRequest(result.Errors.First().Message);
        
        return Ok(result.Value);
    }
}