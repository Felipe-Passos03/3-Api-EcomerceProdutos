namespace _3_Api_EcomerceProdutos.Application.DTOs.Produto;

public class AddProdutoResponse
{
    public Guid ProdutoId { get; set; }
    public string Nome { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Estoque { get; set; }
}