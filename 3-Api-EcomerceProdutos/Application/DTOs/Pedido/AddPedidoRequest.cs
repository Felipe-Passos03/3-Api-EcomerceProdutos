using System.ComponentModel.DataAnnotations;
using _3_Api_EcomerceProdutos.Application.DTOs.Item;

namespace _3_Api_EcomerceProdutos.Application.DTOs.Pedido;

public class AddPedidoRequest
{
    [Required(ErrorMessage = "Campo ClientId obrigatório")]
    public Guid ClientId { get; set; }
    public List<CreateItemPedidoRequest> Itens { get; set; } = new();
}