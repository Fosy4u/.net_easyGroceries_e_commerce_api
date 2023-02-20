using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.Model
{
    public class OrderModel
    {
       
        public Guid CustomerId { get; set; }
        public CartItemModel[] Cart { get; set; } = new CartItemModel[0];
    
       
    }
}