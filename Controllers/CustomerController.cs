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
        //create customers
        public CustomerController(EcommerceDbContext context)
        {
            _context = context;
        }
        //get Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDataModel>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        //get customer by id
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
        //create customer
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
        //update customer
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
        //delete customer
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

// subscribe to royalty membership
         [HttpPut("{id}/royaltyMembership/subscribe")]
        public async Task<IActionResult> SubscribeToRoyaltyMembership( RoyaltySubscriptionModel subscribe)
        {
            var id = subscribe.Id;
            var amountPaid = subscribe.AmountPaid;
            var PaymentMethod = subscribe.PaymentMethod;
            if(!PaymentMethod.Any()) return BadRequest( "Payment method is required");
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound( "Customer not found");
            }
            if(customer.IsRoyaltyMembership)
            {
                return BadRequest( "Customer already has a royalty membership");
            }
            if (amountPaid < 5)
            {
                return BadRequest( "Amount paid is less than £5");
            }
            customer.IsRoyaltyMembership = true;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(customer);
        }
// cancel royalty membership
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