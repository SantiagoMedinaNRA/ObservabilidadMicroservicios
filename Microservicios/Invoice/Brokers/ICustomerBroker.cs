using Invoices.Models;

namespace Invoices.Brokers
{
  public interface ICustomerBroker
  {
    Task<Customer> GetCustomerByIdAsync(int id);
  }
}