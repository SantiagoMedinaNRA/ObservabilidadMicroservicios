using Invoices.Brokers;
using Invoices.Models;

namespace InvoiceS.Services
{
  public class InvoiceService(IStorageBroker storageBroker) : IInvoiceService
  {
    public async ValueTask<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
      return await storageBroker.InsertInvoiceAsync(invoice);
    }

    public async ValueTask<Invoice> GetInvoiceByIdAsync(int id)
    {
      return await storageBroker.SelectInvoiceByIdAsync(id);
    }

    public async ValueTask<List<Invoice>> GetAllInvoicesAsync()
    {
      return await storageBroker.SelectAllInvoicesAsync();
    }

    public async ValueTask<Invoice> UpdateInvoiceAsync(Invoice invoice)
    {
      invoice.UpdatedAt = DateTime.UtcNow;
      return await storageBroker.UpdateInvoiceAsync(invoice);
    }
  }
}