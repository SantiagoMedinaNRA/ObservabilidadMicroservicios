using Customers.Models;

namespace Customers.Services
{
  public interface ICustomerService
  {
    ValueTask<Customer> CreateCustomerAsync(Customer customer);

    ValueTask<Customer> GetCustomerByIdAsync(int id);

    ValueTask<List<Customer>> GetAllCustomersAsync();
  }
}