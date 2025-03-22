using Products.Models;

namespace Products.Brokers
{
  public interface IStorageBroker
  {
    ValueTask<Product> InsertProductAsync(Product product);

    ValueTask<Product> SelectProductByIdAsync(int id);

    ValueTask<List<Product>> SelectAllProductsAsync();
  }
}