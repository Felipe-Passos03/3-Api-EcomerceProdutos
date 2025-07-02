using _3_Api_EcomerceProdutos.Domain;
using _3_Api_EcomerceProdutos.Domain.Model;
using _3_Api_EcomerceProdutos.Domain.Model.Cliente;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;
using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Infra.Data;

public class AppDbContext :  DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ClienteNew?> Clientes { get; set; }
    public DbSet<ProdutoNew> Produtos { get; set; }
    public DbSet<PedidosNew?> Pedidos { get; set; }
    public DbSet<ItensPedidoNew> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClienteNew>(entity =>
        {
            entity.HasKey(client => client.ClientId);
            entity.Property(client => client.Nome).IsRequired();
            entity.Property(client => client.Email).IsRequired();
            entity.Property(client => client.CPF).IsRequired();
            entity.Property(client => client.Senha).IsRequired();
        });
        //Configuração global para salvar no banco de dados todos os campos que são decimais ex: 99.99
        foreach (var property in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal)))
        {
            property.SetColumnType("decimal(18,2)");
        }
        
        modelBuilder.Entity<ProdutoNew>(entity =>
        {
            entity.HasKey(product => product.ProdutoId );
            entity.Property(product => product.Nome).IsRequired();
            entity.Property(product => product.Estoque).IsRequired();
            entity.Property(product => product.PrecoUnitario).IsRequired();
        });
        
        modelBuilder.Entity<PedidosNew>(entity =>
        {
            entity.HasKey(pedidos => pedidos.PedidoId );
            entity.HasMany(pedidos => pedidos.Itens).WithOne(i =>  i.Pedido)
                .HasForeignKey(itens => itens.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            entity.Property(pedidos => pedidos.DataPedido).IsRequired();
            entity.Property(pedidos => pedidos.Desconto).IsRequired();
            entity.Property(pedidos => pedidos.Status).IsRequired();
            entity.Property(pedidos => pedidos.Total).IsRequired();
        });
        
        modelBuilder.Entity<ItensPedidoNew>(entity =>
        {
            entity.HasKey(itens => itens.ItensPedidoId );
        });
    } 

}