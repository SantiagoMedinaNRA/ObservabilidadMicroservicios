using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Products.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController(IProductService productService) : ControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
      var createdProduct = await productService.CreateProductAsync(product);
      return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
      var product = await productService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
      var products = await productService.GetAllProductsAsync();
      return Ok(products);
    }
  }
}