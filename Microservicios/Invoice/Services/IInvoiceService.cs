using Invoices.Models;

namespace InvoiceS.Services
{
  public interface IInvoiceService
  {
    ValueTask<Invoice> CreateInvoiceAsync(Invoice invoice);

    ValueTask<Invoice> GetInvoiceByIdAsync(int id);

    ValueTask<List<Invoice>> GetAllInvoicesAsync();

    ValueTask<Invoice> UpdateInvoiceAsync(Invoice invoice);
  }
}