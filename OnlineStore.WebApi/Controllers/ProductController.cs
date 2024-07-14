using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.AddProduct(product);

        return Ok(result);
    }


    [HttpPut]
    public async Task<IActionResult> Update(int id, int inventoryCount)
    {
        var result = await _productService.UpdateProduct(id, inventoryCount);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await _productService.GetProduct(id);

        return Ok(result);
    }
}