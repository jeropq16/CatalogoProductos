using catalogo.Application.Services;
using catalogo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace catalogo.Api.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create(Product product)
    {
        var createdProduct = await _productService.Create(product);
        if (createdProduct == null) return BadRequest();
        return Ok(createdProduct);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        product.Id = id;
        var updatedProduct = await _productService.Update(product);
        if (updatedProduct == null) return BadRequest();
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedProduct = await _productService.Delete(id);
        if (deletedProduct == null) return BadRequest();
        return Ok(deletedProduct);
    }
}