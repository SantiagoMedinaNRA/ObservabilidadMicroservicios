using System.Text.Json;
using Invoices.Models;

namespace Invoices.Brokers
{
  public class CustomerBroker(HttpClient httpClient) : ICustomerBroker
  {
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
      var response = await httpClient.GetAsync($"api/customers/{id}");

      if (!response.IsSuccessStatusCode)
      {
        return null;
      }

      var content = await response.Content.ReadAsStringAsync();
      return JsonSerializer.Deserialize<Customer>(content, jsonSerializerOptions);
    }

    private readonly JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
  }
}