namespace _3_Api_EcomerceProdutos.Application.Services.Produto;

public class ValidarQuantidadeEstoque
{
    public static void ValidarQtdEstoque(int quantidadeEstoque)
    {
        if(quantidadeEstoque > 1000 )
            throw new ArgumentException("A quantidade máxima de estoque é de 1000 unidades por produto");
    }
}