using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Cliente;

public class CreateClientRequest
{
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Email obrigatório")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "O email não possui um formato válido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "CPF obrigatório")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; }
}