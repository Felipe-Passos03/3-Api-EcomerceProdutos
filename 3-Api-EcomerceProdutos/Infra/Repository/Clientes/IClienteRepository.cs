using _3_Api_EcomerceProdutos.Domain.Model.Cliente;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Clientes;

public interface IClienteRepository
{
    public Task<List<ClienteNew?>> GetAllClientes();
    public Task<ClienteNew> GetClienteById(Guid clientId);
    public Task CreateCliente(ClienteNew? clienteNew);
    public Task UpdateCliente(ClienteNew clienteNew);
    public Task SaveChangesAsync();
    public Task<bool> VerificaClientExistByEmail(string email);
    public Task<bool> VerificaClientExistByCPF(string cpf);
    public Task DeleteClienteById(Guid clienteId);
    //public Task<List<Cliente>> GetOrdersByClientId(Guid clientId);  --> Vai ficar na order
}