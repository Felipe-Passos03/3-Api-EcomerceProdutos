using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Item;

public class EditarItensPedidoRequest
{
    [Required(ErrorMessage = "Selecione o id do produto")]
    public Guid ProdutoId { get; set; }
    
    [Required(ErrorMessage = "Selecione a quantidade")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0")]
    public int Quantidade { get; set; }
    
}