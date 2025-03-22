using Products.Brokers;
using Products.Models;

namespace Products.Services
{
  public class ProductService(IStorageBroker storageBroker) : IProductService
  {
    public async ValueTask<Product> CreateProductAsync(Product product)
    {
      return await storageBroker.InsertProductAsync(product);
    }

    public async ValueTask<Product> GetProductByIdAsync(int id)
    {
      return await storageBroker.SelectProductByIdAsync(id);
    }

    public async ValueTask<List<Product>> GetAllProductsAsync()
    {
      return await storageBroker.SelectAllProductsAsync();
    }
  }
}