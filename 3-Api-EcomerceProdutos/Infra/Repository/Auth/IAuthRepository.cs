using _3_Api_EcomerceProdutos.Domain.Model.Cliente;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Auth;

public interface IAuthRepository
{
    public Task<ClienteNew?> Login(string email);
}