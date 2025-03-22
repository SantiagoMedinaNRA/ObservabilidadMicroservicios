using Products.Models;
using Products.Storage;

namespace Products.Brokers
{
  public class StorageBroker(ProductDbContext dbContext) : IStorageBroker
  {
    public async ValueTask<Product> InsertProductAsync(Product product)
    {
      dbContext.Products.Add(product);
      await dbContext.SaveChangesAsync();
      return product;
    }

    public async ValueTask<Product> SelectProductByIdAsync(int id)
    {
      return await dbContext.Products.FindAsync(id);
    }

    public async ValueTask<List<Product>> SelectAllProductsAsync()
    {
      return dbContext.Products.ToList();
    }
  }
}