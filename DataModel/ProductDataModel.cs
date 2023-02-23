using System.ComponentModel.DataAnnotations;
namespace easyGroceries_e_commerce_api.DataModel

// model for product
// would be best for colors, and size to have a separate model allowing stock quantity to be tracked properly
// another option is have each item added inidividually to database even when they are the same product
// this would allow for more accurate stock tracking
// left it like this due to time constraints
{
    public class ProductDataModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; } = 0;
        public string ImageUrl { get; set; } = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg";
        public string? Category { get; set; }
        public string? ProductBrand { get; set; }
        public int StockQuantity { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Colors { get; set; } 
        public string? Sizes { get; set; } 
       
        
    }
}