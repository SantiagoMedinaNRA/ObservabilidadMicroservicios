using Invoices.Models;

namespace Invoices.Brokers
{
  public interface IStorageBroker
  {
    ValueTask<Invoice> InsertInvoiceAsync(Invoice invoice);

    ValueTask<Invoice> SelectInvoiceByIdAsync(int id);

    ValueTask<List<Invoice>> SelectAllInvoicesAsync();

    ValueTask<Invoice> UpdateInvoiceAsync(Invoice invoice);
  }
}