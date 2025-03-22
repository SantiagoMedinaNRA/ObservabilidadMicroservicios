using Customers.Brokers;
using Customers.Models;

namespace Customers.Services
{
  public class CustomerService(IStorageBroker storageBroker) : ICustomerService
  {
    public async ValueTask<Customer> CreateCustomerAsync(Customer customer)
    {
      return await storageBroker.InsertCustomerAsync(customer);
    }

    public async ValueTask<Customer> GetCustomerByIdAsync(int id)
    {
      return await storageBroker.SelectCustomerByIdAsync(id);
    }

    public async ValueTask<List<Customer>> GetAllCustomersAsync()
    {
      return await storageBroker.SelectAllCustomersAsync();
    }
  }
}