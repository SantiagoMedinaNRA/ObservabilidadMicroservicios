using Customers.Models;
using Customers.Storage;

namespace Customers.Brokers
{
  public class StorageBroker(CustomerDbContext dbContext) : IStorageBroker
  {
    public async ValueTask<Customer> InsertCustomerAsync(Customer customer)
    {
      dbContext.Customers.Add(customer);
      await dbContext.SaveChangesAsync();
      return customer;
    }

    public async ValueTask<Customer> SelectCustomerByIdAsync(int id)
    {
      return await dbContext.Customers.FindAsync(id);
    }

    public async ValueTask<List<Customer>> SelectAllCustomersAsync()
    {
      return dbContext.Customers.ToList();
    }
  }
}