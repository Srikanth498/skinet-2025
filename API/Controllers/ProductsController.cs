using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, 
        string? type, string? sort) // api/products
    {

        return Ok(await repo.GetProductsAsync(brand, type, sort));//return all products, Wrapped in Ok() to pass type issues with ActionResult.
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Product = await repo.GetProductByIdAsync(id);

        if(Product == null) return NotFound(); // if product is not found or null

        return Product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if(await repo.SavechangesAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if(product.Id !=id || !productExists(id)) return BadRequest("Cannot update product");

        repo.updateProduct(product);

        if(await repo.SavechangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product =await repo.GetProductByIdAsync(id);

        if(product == null) return NotFound();
        repo.DeleteProduct(product);

        if(await repo.SavechangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    private bool productExists(int id)
    {
        return repo.productExists(id);
    }
}

