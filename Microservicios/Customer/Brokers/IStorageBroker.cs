using Customers.Models;

namespace Customers.Brokers
{
  public interface IStorageBroker
  {
    ValueTask<Customer> InsertCustomerAsync(Customer customer);

    ValueTask<Customer> SelectCustomerByIdAsync(int id);

    ValueTask<List<Customer>> SelectAllCustomersAsync();
  }
}