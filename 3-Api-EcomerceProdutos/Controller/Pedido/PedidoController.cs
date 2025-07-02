using _3_Api_EcomerceProdutos.Application.DTOs.Item;
using _3_Api_EcomerceProdutos.Application.DTOs.Pedido;
using _3_Api_EcomerceProdutos.Application.Interface.Pedido;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3_Api_EcomerceProdutos.Controller.Pedido;

[ApiController]
[Route("[Controller]")]
[Authorize(Roles = "User")]
public class PedidoController : ControllerBase
{
    private readonly ICriarCarrinhoService  _criarCarrinhoService;
    private readonly IDeletePedidoByIdService  _deletePedidoByIdService;
    private readonly IGetAllPedidosService  _getAllPedidosService;
    private readonly IFinalizarCompraService  _finalizarCompraService;
    private readonly IGetCarrinhoByIdService  _getCarrinhoByIdService;
    private readonly IEditarCarrinhoService  _editarCarrinhoService;

    public PedidoController(ICriarCarrinhoService criarCarrinhoService,
                            IDeletePedidoByIdService deletePedidoByIdService,
                            IGetAllPedidosService getAllPedidosService,
                            IFinalizarCompraService finalizarCompraService,
                            IGetCarrinhoByIdService getCarrinhoByIdService,
                            IEditarCarrinhoService editarCarrinhoService)
    {
        _criarCarrinhoService = criarCarrinhoService;
        _deletePedidoByIdService = deletePedidoByIdService;
        _getAllPedidosService = getAllPedidosService;
        _finalizarCompraService = finalizarCompraService;
        _getCarrinhoByIdService = getCarrinhoByIdService;
        _editarCarrinhoService = editarCarrinhoService;
    }

    [HttpPost]
    [Route("criarCarrinhoDeCompra")]
    
    public async Task<IActionResult> CreatePedido([FromBody]AddPedidoRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        if (request.ClientId == Guid.Empty)
            return BadRequest("Client ID obrigatório");
        
        var createOrder = _criarCarrinhoService.AddPedidoAoCarrinhoAsync(request);
        if(createOrder.Result.IsFailed)
            return BadRequest(createOrder.Result.Errors.First().Message);
            
        return Ok("Pedido criado com sucesso");
    }

    [HttpDelete]
    [Route("excluirCarrinhoDeCompra")]
    public async Task<IActionResult> DeletePedido([FromQuery] Guid pedidoId)
    {
        if(pedidoId == Guid.Empty)
            return BadRequest("Preencha o campo idPedido");
        
        var delete = _deletePedidoByIdService.DeletePedidoById(pedidoId);
        if(delete.Result.IsFailed)
            return BadRequest(delete.Result.Errors.First().Message);
        
        return Ok("Pedido removido com sucesso");
    }

    [HttpGet]
    [Route("getAllCarrinhosDeCompra")]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _getAllPedidosService.GetAllPedidos();
        if(pedidos == null)
            return NotFound();
        
        return Ok(pedidos.Value);
    }
    [HttpPut]
    [Route("finalizarCarrinhoDeCompra")]
    public async Task<IActionResult> FinalizarCompraAync([FromQuery]Guid pedidoId)
    {
        if(pedidoId == Guid.Empty)
            return  BadRequest("Preencha o campo idPedido");
        
        var finalizarCompra = await _finalizarCompraService.FinalizarCompra(pedidoId);
        
        if(finalizarCompra.IsFailed)
            return BadRequest(finalizarCompra.Errors.First().Message);
        
        return Ok("Compra realizado com sucesso");
    }

    [HttpGet]
    [Route("getCarrinhoById")]
    public async Task<IActionResult> GetCarrinhoById(Guid pedidoId)
    {
        var result = await _getCarrinhoByIdService.GetCarrinhoById(pedidoId);
        if (result.IsFailed)
            return BadRequest(result.Errors.First().Message);
        
        return Ok(result.Value);
    }

    [HttpPost]
    [Route("addItensAoCarrinhoExistente")]
    public async Task<IActionResult> EditarCarrinhoById([FromBody] ItensRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(modelState: ModelState);

        var result = await _editarCarrinhoService.EditarItensNoCarrinho(request);
        
        if(result.IsFailed)
            return BadRequest(result.Errors.First().Message);
        
        return Ok("Carrinho editado com sucesso ");
    }
}