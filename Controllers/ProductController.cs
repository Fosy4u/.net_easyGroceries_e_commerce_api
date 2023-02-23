using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DataModel;
using easyGroceries_e_commerce_api.Model;
using easyGroceries_e_commerce_api.DatabaseContext;
using Newtonsoft.Json;

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
           
              var products =  await _context.Products.ToListAsync();
          var productList = new List<ProductResponseModel>();
            foreach (var product in products)
            {
                var productResponseModel = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Category = product.Category,
                    StockQuantity = product.StockQuantity,
                    ProductBrand = product.ProductBrand,
                    CreatedAt = product.CreatedAt,
                    Colors = product.Colors == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(product.Colors),
                    Sizes = product.Sizes == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(product.Sizes)
                };
                productList.Add(productResponseModel);
            }
            return Ok(productList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDataModel>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var productResponseModel = new ProductResponseModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                ProductBrand = product.ProductBrand,
                CreatedAt = product.CreatedAt,
                Colors = product.Colors == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(product.Colors),
                Sizes = product.Sizes == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(product.Sizes)
            };
            return Ok(productResponseModel);
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
                ProductBrand = product.ProductBrand,
                Colors = product.Colors == null ? null : JsonConvert.SerializeObject(product.Colors),
                Sizes = product.Sizes == null ? null : JsonConvert.SerializeObject(product.Sizes)

                
            };
            _context.Products.Add(productDataModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = productDataModel.Id }, productDataModel);
            
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
             return Ok("deleted");
        }
     }
}

