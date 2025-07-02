using System.Text;
using _3_Api_EcomerceProdutos.Application.Interface;
using _3_Api_EcomerceProdutos.Application.Services;
using _3_Api_EcomerceProdutos.Application.Interface.Auth;
using _3_Api_EcomerceProdutos.Application.Interface.Cliente;
using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using _3_Api_EcomerceProdutos.Application.Services.Auth;
using _3_Api_EcomerceProdutos.Application.Services.Cliente;
using _3_Api_EcomerceProdutos.Application.Services.Pedido;
using _3_Api_EcomerceProdutos.Application.Services.Produto;
using _3_Api_EcomerceProdutos.Domain.Model.JWTToken;
using _3_Api_EcomerceProdutos.Domain.Service;
using _3_Api_EcomerceProdutos.Infra.Auth;
using _3_Api_EcomerceProdutos.Infra.Data;
using _3_Api_EcomerceProdutos.Infra.Repository.Auth;
using _3_Api_EcomerceProdutos.Infra.Repository.Clientes;
using _3_Api_EcomerceProdutos.Infra.Repository.Itens;
using _3_Api_EcomerceProdutos.Infra.Repository.Pedidos;
using _3_Api_EcomerceProdutos.Infra.Repository.Produtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidosRepository, PedidosRepository>();
builder.Services.AddScoped<IItensRepository, ItensRepository>();

builder.Services.AddScoped<IAddClientService, AddClientService>();
builder.Services.AddScoped<IGetAllClientesService, GetAllClientesService>();
builder.Services.AddScoped<IGetClientesByIdService, GetClientesByIdService>();
builder.Services.AddScoped<IUpdateClienteService, UpdateClienteService>();
builder.Services.AddScoped<IDeleteClientService,  DeleteClientService>();

builder.Services.AddScoped<IAddProdutoService, AddProdutoService>();
builder.Services.AddScoped<IGetAllProductsService, GetAllProductsService>();
builder.Services.AddScoped<IGetProductByIdService, GetProductByIdService>();
builder.Services.AddScoped<IDeleteProductByIdService, DeleteProductByIdService>();
builder.Services.AddScoped<IUpdateProductService, UpdateProductService>();

builder.Services.AddScoped<ICriarCarrinhoService, CriarCarrinhoService>();
builder.Services.AddScoped<IDeletePedidoByIdService, DeletePedidoByIdService>();
builder.Services.AddScoped<IGetAllPedidosService, GetAllPedidoService>();
builder.Services.AddScoped<IFinalizarCompraService, FinalizarCompraService>();
builder.Services.AddScoped<IGetCarrinhoByIdService, GetCarrinhoByIdService>();
builder.Services.AddScoped<IEditarCarrinhoService, EditarCarrinhoService>();
    
    ;
builder.Services.AddScoped<ILoginService, LoginService>();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite
        (builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    .LogTo(Console.WriteLine, LogLevel.Error));;

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
