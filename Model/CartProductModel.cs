using easyGroceries_e_commerce_api.DataModel;
using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.Model
{
    public class CartProductModel

    {
        [Key]
        public Guid Id { get; set; }
        public ProductDataModel Product { get; set; } = new ProductDataModel();
        public int Quantity { get; set; }
        public float UnitPrice { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;
        public bool IsDiscounted { get; set; } = false;
        public float? DiscountedPrice { get; set; }
        public float? DiscountPercentage { get; set; } 
        public string? Color { get; set; }
        public string? Size { get; set; }

    }
}