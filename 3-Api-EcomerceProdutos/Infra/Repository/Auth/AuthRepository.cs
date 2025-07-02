using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Repository.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ClienteNew?> Login(string email)
    {
        return await _context.Clientes.FirstOrDefaultAsync(login => login.Email == email);
    }
}