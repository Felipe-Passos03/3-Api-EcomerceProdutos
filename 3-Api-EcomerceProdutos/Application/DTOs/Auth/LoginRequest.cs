using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Email obrigatório")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "O email não possui um formato válido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Senha obrigatória")]
    public string Senha { get; set; }
}