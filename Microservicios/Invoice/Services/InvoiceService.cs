using Invoices.Brokers;
using Invoices.Models;

namespace InvoiceS.Services
{
  public class InvoiceService(IStorageBroker storageBroker, IProductBroker productBroker,
    ICustomerBroker customerBroker) : IInvoiceService
  {
    public async ValueTask<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
      // Validate customer
      var customer = await customerBroker.GetCustomerByIdAsync(invoice.CustomerId);

      if (customer == null)
      {
        throw new KeyNotFoundException("Customer not found.");
      }

      // Validate products
      if (invoice.ProductIds != null && invoice.ProductIds.Count > 0)
      {
        invoice.Products = await GetProductsAsync(invoice.ProductIds);

        if (invoice.TotalAmount == 0)
        {
          invoice.TotalAmount = invoice.Products.Sum(x => x.Price);
        }
      }

      return await storageBroker.InsertInvoiceAsync(invoice);
    }

    public async ValueTask<Invoice> GetInvoiceByIdAsync(int id)
    {
      Invoice invoice = await storageBroker.SelectInvoiceByIdAsync(id);

      if (invoice != null)
      {
        // Fetch customer details
        invoice.Customer = await customerBroker.GetCustomerByIdAsync(invoice.CustomerId);

        // Fetch product details
        if (invoice.ProductIds != null && invoice.ProductIds.Count > 0)
        {
          invoice.Products = await GetProductsAsync(invoice.ProductIds);
        }
      }

      return invoice;
    }

    public async ValueTask<List<Invoice>> GetAllInvoicesAsync()
    {
      var invoices = await storageBroker.SelectAllInvoicesAsync();

      foreach (var invoice in invoices)
      {
        // Fetch customer details for each invoice
        invoice.Customer = await customerBroker.GetCustomerByIdAsync(invoice.CustomerId);

        // Fetch product details
        if (invoice.ProductIds != null && invoice.ProductIds.Count > 0)
        {
          invoice.Products = await GetProductsAsync(invoice.ProductIds);
        }
      }

      return invoices;
    }

    public async ValueTask<Invoice> UpdateInvoiceAsync(Invoice invoice)
    {
      // Validate customer
      var customer = await customerBroker.GetCustomerByIdAsync(invoice.CustomerId);
      if (customer == null)
      {
        throw new KeyNotFoundException("Customer not found.");
      }

      // Validate products
      if (invoice.ProductIds != null && invoice.ProductIds.Count > 0)
      {
        invoice.Products = await GetProductsAsync(invoice.ProductIds);
      }

      invoice.UpdatedAt = DateTime.UtcNow;
      invoice.TotalAmount = invoice.Products.Sum(x => x.Price);

      return await storageBroker.UpdateInvoiceAsync(invoice);
    }

    private async ValueTask<List<Product>> GetProductsAsync(List<int> productIds)
    {
      var products = new List<Product>(productIds?.Count ?? 0);
      foreach (var productId in productIds)
      {
        var product = await productBroker.GetProductByIdAsync(productId);
        if (product != null)
        {
          products.Add(product);
        }
        else
        {
          throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }
      }
      return products;
    }
  }
}