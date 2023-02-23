

namespace easyGroceries_e_commerce_api.Model
{
    public class CartItemModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public float Rate { get; set; } = 0;

    }
}