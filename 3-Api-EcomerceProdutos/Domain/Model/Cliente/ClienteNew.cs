namespace _3_Api_EcomerceProdutos.Domain.Model.Cliente;

public class ClienteNew
{
    public Guid ClientId { get; init; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    
    public string Senha { get; private set; }
    public string CPF { get; private set; }

    public ClienteNew()
    {
        
    }

    public ClienteNew(string nome, string email, string cpf, string senha)
    {
        ClientId = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
        CPF = cpf;
    }

    public void UpdateCliente(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
    }
    
}