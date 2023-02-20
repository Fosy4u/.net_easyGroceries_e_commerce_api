using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DataModel;
using easyGroceries_e_commerce_api.Model;
using easyGroceries_e_commerce_api.DatabaseContext;

namespace easyGroceries_e_commerce_api.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        public ProductController(EcommerceDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDataModel>>> GetProducts()
        {
            return  await _context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDataModel>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {
            var productDataModel = new ProductDataModel
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                ProductBrand = product.ProductBrand
                
            };
            _context.Products.Add(productDataModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = productDataModel.Id }, productDataModel);
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDataModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

