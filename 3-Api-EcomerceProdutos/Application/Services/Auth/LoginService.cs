using _3_Api_EcomerceProdutos.Application.DTOs.Auth;
using _3_Api_EcomerceProdutos.Application.Interface.Auth;
using _3_Api_EcomerceProdutos.Domain.Service;
using _3_Api_EcomerceProdutos.Infra.Repository.Auth;

namespace _3_Api_EcomerceProdutos.Application.Services.Auth;

public class LoginService: ILoginService
{
    private readonly IAuthRepository _authRepository;

    public LoginService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        
            var auth = await _authRepository.Login(request.Email);

            if (auth != null && BCrypt.Net.BCrypt.Verify(request.Senha, auth.Senha))
            {
                var token = TokenService.GenerateToken(request.Email);
            
                return new LoginResponse
                {   Token = token, 
                    Tipo = "User" 
                };
            }

            return null;
    }
}