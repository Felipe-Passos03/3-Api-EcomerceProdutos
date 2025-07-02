using _3_Api_EcomerceProdutos.Application.DTOs.Produto;
using _3_Api_EcomerceProdutos.Application.Interface.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3_Api_EcomerceProdutos.Controller.Produto;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class ProductController : ControllerBase
{
    private readonly IAddProdutoService _addProdutoService;
    private readonly IGetAllProductsService  _getAllProductsService;
    private readonly IGetProductByIdService   _getProductByIdService;
    private readonly IDeleteProductByIdService   _deleteProductByIdService;
    private readonly IUpdateProductService   _updateProductService;

    public ProductController(IAddProdutoService addProdutoService, 
                            IGetAllProductsService getAllProductsService,
                            IGetProductByIdService getProductByIdService,
                            IDeleteProductByIdService deleteProductByIdService,
                            IUpdateProductService updateProductService)
    {
        _addProdutoService = addProdutoService;
        _getAllProductsService = getAllProductsService;
        _getProductByIdService = getProductByIdService;
        _deleteProductByIdService = deleteProductByIdService;
        _updateProductService = updateProductService;
    }

    [HttpPost]
    [Route("addProduto")]
    public async Task<IActionResult> AddProduto([FromBody] AddProdutoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        var addProdutoResponse = await _addProdutoService.AddProduto(request);
        
        if (addProdutoResponse.IsFailed)
            return BadRequest(addProdutoResponse.Errors.First().Message);
        
        return Ok(addProdutoResponse.Value);
    }

    [HttpGet]
    [Route("getAllProdutos")]
    public async Task<IActionResult> GetAllProdutos()
    {
        var productList = await _getAllProductsService.GetAllProducts();
        return  Ok(productList.Value);
    }
    
    [HttpGet]
    [Route("getProductById")]
    public async Task<IActionResult> GetProductById([FromQuery]Guid produtoId)
    {
        if(produtoId == Guid.Empty)
            return BadRequest("IdProduto é vazio, preencha com um ID válido");
        
        var getProductByIdResponse = await _getProductByIdService.GetProductById(produtoId);
        return Ok(getProductByIdResponse.Value);
    }

    [HttpDelete]
    [Route("removeProduto")]
    public async Task<IActionResult> RemoveProdutoById([FromQuery] Guid produtoId)
    {
        if(produtoId == Guid.Empty)
            return BadRequest("IdProduto é vazio, preencha com um ID válido");
        
        var delete = await _deleteProductByIdService.DeleteProductById(produtoId);
        
        if(delete.IsFailed)
            return BadRequest(delete.Errors.First().Message);
        
        return Ok("Produto removido com sucesso");
    }

    [HttpPost]
    [Route("updateProduto")]
    public async Task<IActionResult> UpdateProdutoAsync([FromBody] UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(modelState: ModelState);
        
        if(request.Id == Guid.Empty)
            return BadRequest("IdProduto é vazio, preencha com um ID válido");
        
        var updateProductResponse = await _updateProductService.UpdateProduct(request);
        
        if(updateProductResponse.IsFailed)
            return BadRequest(updateProductResponse.Errors.First().Message);
        
        return Ok("Produto atualizado com sucesso");
    }
}