using _3_Api_EcomerceProdutos.Domain.Model.Cliente;

namespace _3_Api_EcomerceProdutos.Domain.Model.Produto;

public class ProdutoNew
{
    public Guid ProdutoId { get; init; }
    public string Nome { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public int Estoque { get; private set; }


    public ProdutoNew(string nome, decimal precoUnitario, int estoque)
    {
        ProdutoId = Guid.NewGuid();
        Nome = nome;
        PrecoUnitario = precoUnitario;
        Estoque = estoque;
    }

    public void AtualizarProduto(string nome, decimal precoUnitario, int estoque)
    {
        Nome = nome;
        PrecoUnitario = precoUnitario;
        Estoque = estoque;
    }
    
    public void DescontarEstoque(int quantidade)
    {
        Estoque -= quantidade;
    }
}