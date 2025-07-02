using System.ComponentModel.DataAnnotations;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Item;

public class ItensRequest
{
    [Required(ErrorMessage = "Selecione o id do pedido")]
    public Guid PedidoId { get; set; }
    
    public List<EditarItensPedidoRequest> EditarItens { get; set; } = new();
}