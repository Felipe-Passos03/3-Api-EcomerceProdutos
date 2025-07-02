using _3_Api_EcomerceProdutos.Domain.Enums;
using _3_Api_EcomerceProdutos.Domain.Model.Itens;
using _3_Api_EcomerceProdutos.Domain.Model.Produto;
using Microsoft.EntityFrameworkCore;

namespace _3_Api_EcomerceProdutos.Domain.Model.Pedido;

public class PedidosNew
{
    public Guid PedidoId { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime DataPedido { get; private set; }
    public decimal Total { get; private set; }
    public decimal Desconto { get; private set; } = decimal.Zero;
    public StatusPedido Status { get; private set; }
    public List<ItensPedidoNew> Itens { get; private set; } = new();

    public PedidosNew(Guid clientId, decimal total, decimal desconto)
    {
        PedidoId = Guid.NewGuid();
        ClientId = clientId;
        Total = total;
        Desconto = 0;
        Status = StatusPedido.EmProcessamento;
        DataPedido = DateTime.Now;
        Itens = new List<ItensPedidoNew>();
    }

    public void DefinirDesconto(decimal desconto)
    {
        Desconto = desconto;
    }
    public decimal CalcularDesconto(decimal subtotalItem)
    {
        var descontoPercentual = 10;
        if (subtotalItem > 500)
        {
            var desconto =  (subtotalItem * descontoPercentual) / 100;
            DefinirDesconto(desconto);
            return desconto;
        }
            return 0;
            
    }

    public void FinalizarPedido(decimal subtotalItem)
    {
        Status = StatusPedido.PedidoFinalizado;
        Total = subtotalItem - Desconto;
    }
    
    public void CriarCarrinhoComCalculoDesconto(ItensPedidoNew item)
    {
        item.PedidoId = this.PedidoId;
        
        var desconto = CalcularDesconto(item.Subtotal);
        DefinirDesconto(desconto);

        Itens.Add(item);
    }
    
    public void CalcularTotal()
    {
        var subtotal = Itens.Sum(i => i.Subtotal);
        Total = subtotal - Desconto;
    }
}