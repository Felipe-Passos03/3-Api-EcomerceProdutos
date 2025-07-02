using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Clientes;

public class ClienteRepository :  IClienteRepository
{
    
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ClienteNew?>> GetAllClientes()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<ClienteNew> GetClienteById(Guid clientId)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.ClientId == clientId);
    }

    public async  Task CreateCliente(ClienteNew? clienteNew)
    {
        await _context.Clientes.AddAsync(clienteNew);
    }

    public Task UpdateCliente(ClienteNew clienteNew)
    {
        _context.Update(clienteNew);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
       await _context.SaveChangesAsync();
    }

    public async Task<bool> VerificaClientExistByEmail(string email)
    {
        return await _context.Clientes.AnyAsync(c => c.Email == email);
    }

    public async Task<bool> VerificaClientExistByCPF(string cpf)
    {
        return await _context.Clientes.AnyAsync(c => c.CPF == cpf);
    }

    public async Task DeleteClienteById(Guid clienteId)
    {
         _context.Clientes.Remove(await _context.Clientes.FindAsync(clienteId));
    }
}