using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Item;

public class CreateItemPedidoRequest
{
    [Required(ErrorMessage = "Campo produtoID obrigatório")]
    public Guid ProdutoId { get; set; }
    
    
    [Required(ErrorMessage = "Selecione uma quantidade")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0")]
    public int Quantidade { get; set; }
    
}