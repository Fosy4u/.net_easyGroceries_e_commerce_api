using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.Model
{
    public class ProductResponseModel
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
        public List <string>? Colors { get; set; } 
        public List <string>? Sizes { get; set; } 
    }
}