using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Produto;

public class UpdateProductRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Campo nome é obrigatório")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Campo preço unitário é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal PrecoUnitario { get; set; }
    
    [Required(ErrorMessage = "Campo estoque obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O estoque deve ser maior que zero")]
    public int Estoque { get; set; }
}