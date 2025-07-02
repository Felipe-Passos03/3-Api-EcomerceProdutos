namespace _3_Api_EcomerceProdutos.Application.DTOs.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
}