namespace Invoices.Models
{
  public class Invoice
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Default status
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // List of product IDs included in the invoice
    public List<int> ProductIds { get; set; } = [];

    // Navigation properties (not stored in the database)
    public Customer Customer { get; set; }

    public List<Product> Products { get; set; } = [];
  }
}