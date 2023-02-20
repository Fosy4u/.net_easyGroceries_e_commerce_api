using System.ComponentModel.DataAnnotations;
using easyGroceries_e_commerce_api.Model;
using Newtonsoft.Json;

namespace easyGroceries_e_commerce_api.DataModel
{
    public class OrderDataModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReceiptNo { get; set; } = "";
        [Required]
        public Guid CustomerId { get; set; } 
        public Guid OrderRegNo { get; set; } 
        public string? Cart { get; set; }
        public float Total { get; set; } = 0;
        public bool IsDiscounted { get; set; } = false;
        public float? DiscountedTotal { get; set; }
        public float? DiscountPercentage { get; set; }
        public float AmountDue { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }  = DateTime.Now;
        public DateTime? ShippedAt { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public string? PaymentMethod { get; set; }
        public string PaymentStatus { get; set; } = "unpaid";
        public string PaymentCurrency { get; set; } = "£";
        public Guid PaymentReceiptId { get; set; } 
        

     
        
    }
    
}