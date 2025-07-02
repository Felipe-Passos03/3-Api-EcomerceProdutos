namespace _3_Api_EcomerceProdutos.Application.DTOs.Cliente;

public class CreateClientResponse
{
    public Guid ClienteId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Senha { get; set; }
}