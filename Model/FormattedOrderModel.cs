using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.Model
{
    public class FormattedOrderModel
    {
        [Key]
        public int Id { get; set; }
        public string ReceiptNo { get; set; } = "";
        public Guid OrderRegNo { get; set; } 
        public List<CartProductModel> Cart { get; set; } = new List<CartProductModel>();
        public float Total { get; set; } = 0;
        public bool IsDiscounted { get; set; } = false;
        public float? DiscountedTotal { get; set; }
        public float? DiscountPercentage { get; set; }
        public float AmountDue { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }  = DateTime.Now;
        public DateTime? ShippedAt { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CanceledAt { get; set; } 
        public string PaymentStatus { get; set; } = "unpaid";
      
       
        
    }
}