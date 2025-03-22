using System.Text.Json;
using Invoices.Models;

namespace Invoices.Brokers
{
  public class ProductBroker(HttpClient httpClient) : IProductBroker
  {
    public async Task<Product> GetProductByIdAsync(int id)
    {
      var response = await httpClient.GetAsync($"api/products/{id}");

      if (!response.IsSuccessStatusCode)
      {
        return null;
      }

      var content = await response.Content.ReadAsStringAsync();
      return JsonSerializer.Deserialize<Product>(content, jsonSerializerOptions);
    }

    private readonly JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
  }
}