using Products.Models;

namespace Products.Services
{
  public interface IProductService
  {
    ValueTask<Product> CreateProductAsync(Product product);

    ValueTask<Product> GetProductByIdAsync(int id);

    ValueTask<List<Product>> GetAllProductsAsync();
  }
}