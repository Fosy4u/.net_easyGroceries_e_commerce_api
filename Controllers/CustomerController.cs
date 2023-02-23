using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DataModel;
using easyGroceries_e_commerce_api.Model;
using easyGroceries_e_commerce_api.DatabaseContext;

namespace easyGroceries_e_commerce_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        public CustomerController(EcommerceDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDataModel>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDataModel>> GetCustomer(Guid id)
        {
           
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound( "Customer not found");
            }
            return customer;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel customer)
        {
            if(!customer.FirstName.Any() || !customer.LastName.Any()) return BadRequest();
            var customerDataModel = new CustomerDataModel
            {
                Id = Guid.NewGuid(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PostCode = customer.PostCode,
                City = customer.City,
                Country = customer.Country,
                Address = customer.Address,
                Phone = customer.Phone,
                ImageUrl = customer.ImageUrl,
                IsRoyaltyMembership = customer.IsRoyaltyMembership || false
            };
            _context.Customers.Add(customerDataModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customerDataModel.Id }, customerDataModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerDataModel customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
             return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound( "Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
          return Ok("deleted");
        }

         [HttpPut("{id}/royaltyMembership/subscribe")]
        public async Task<IActionResult> SubscribeToRoyaltyMembership(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound( "Customer not found");
            }
            customer.IsRoyaltyMembership = true;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut("{id}/royaltyMembership/cancel")]
        public async Task<IActionResult> CancelRoyaltyMembership(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound( "Customer not found");
            }
            customer.IsRoyaltyMembership = false;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
             return Ok(customer);
        }
        
    }
}