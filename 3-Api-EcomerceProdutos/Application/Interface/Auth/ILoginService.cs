using _3_Api_EcomerceProdutos.Application.DTOs.Auth;

namespace _3_Api_EcomerceProdutos.Application.Interface.Auth;

public interface ILoginService
{
    public Task<LoginResponse> LoginAsync(LoginRequest request);
}