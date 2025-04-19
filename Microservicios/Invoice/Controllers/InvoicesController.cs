using Invoices.Models;
using InvoiceS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoices.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
  {
    [HttpGet("health")]
    [Produces("text/plain")]
    public IActionResult Health()
    {
      return Ok("Prueba video");
    }

    [HttpGet]
    public async Task<ActionResult<List<Invoice>>> GetAllInvoices()
    {
      try
      {
        var invoices = await invoiceService.GetAllInvoicesAsync();
        return Ok(invoices);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
    {
      try
      {
        var invoice = await invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null)
        {
          return NotFound();
        }
        return Ok(invoice);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
    {
      try
      {
        var createdInvoice = await invoiceService.CreateInvoiceAsync(invoice);
        return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.Id }, createdInvoice);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Invoice>> UpdateInvoice(int id, Invoice invoice)
    {
      try
      {
        if (id != invoice.Id)
        {
          return BadRequest();
        }

        var updatedInvoice = await invoiceService.UpdateInvoiceAsync(invoice);
        return Ok(updatedInvoice);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}