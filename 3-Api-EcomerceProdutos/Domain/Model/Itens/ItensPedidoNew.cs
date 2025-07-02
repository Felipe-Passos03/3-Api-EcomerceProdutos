using System.Text.Json.Serialization;
using _3_Api_EcomerceProdutos.Domain.Model.Pedido;

namespace _3_Api_EcomerceProdutos.Domain.Model.Itens;

public class ItensPedidoNew
{
        public Guid ItensPedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Subtotal { get; private set; }
        public Guid PedidoId { get;  set; }

        
        [JsonIgnore]
        public PedidosNew Pedido { get; private set; }
        
        private ItensPedidoNew()
        {
            
        }
        public ItensPedidoNew(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            ItensPedidoId = Guid.NewGuid();
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Subtotal = quantidade * precoUnitario;
        }

        public void AtualizarItens(int quantidade, decimal precoUnitario)
        {
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Subtotal = quantidade * precoUnitario;
        }

}