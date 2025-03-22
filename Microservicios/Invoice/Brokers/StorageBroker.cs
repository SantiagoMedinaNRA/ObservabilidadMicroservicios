using Invoices.Models;
using Invoices.Storage;

namespace Invoices.Brokers
{
  public class StorageBroker(InvoiceDbContext dbContext) : IStorageBroker
  {
    public async ValueTask<Invoice> InsertInvoiceAsync(Invoice invoice)
    {
      dbContext.Invoices.Add(invoice);
      await dbContext.SaveChangesAsync();
      return invoice;
    }

    public async ValueTask<Invoice> SelectInvoiceByIdAsync(int id)
    {
      return await dbContext.Invoices.FindAsync(id);
    }

    public async ValueTask<List<Invoice>> SelectAllInvoicesAsync()
    {
      return dbContext.Invoices.ToList();
    }

    public async ValueTask<Invoice> UpdateInvoiceAsync(Invoice invoice)
    {
      dbContext.Invoices.Update(invoice);
      await dbContext.SaveChangesAsync();
      return invoice;
    }
  }
}