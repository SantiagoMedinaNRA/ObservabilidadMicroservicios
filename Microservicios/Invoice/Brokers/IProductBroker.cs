using Invoices.Models;

namespace Invoices.Brokers
{
  public interface IProductBroker
  {
    Task<Product> GetProductByIdAsync(int id);
  }
}