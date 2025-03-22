using Customers.Models;
using Customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CustomersController(ICustomerService customerService) : ControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
      var createdCustomer = await customerService.CreateCustomerAsync(customer);
      return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
      var customer = await customerService.GetCustomerByIdAsync(id);
      if (customer == null)
      {
        return NotFound();
      }
      return Ok(customer);
    }

    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAllCustomers()
    {
      var customers = await customerService.GetAllCustomersAsync();
      return Ok(customers);
    }
  }
}