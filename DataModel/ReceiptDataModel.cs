using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.DataModel
{
    public class ReceiptDataModel
    {
        [Key]
        public Guid Id { get; set; } 
        [Required]
        public string ReceiptNo { get; set; } = "";
        [Required]
        public CustomerDataModel Customer { get; set; } = new CustomerDataModel();
        [Required]
        public Guid TransactionReference { get; set; }
        public int OrderId { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }  = DateTime.Now;
        [Required]
        public string? PaymentMethod { get; set; }
        public string PaymentStatus { get; set; } = "unpaid";
        public string PaymentCurrency { get; set; } = "£";
        public float Total { get; set; } = 0;
        public bool IsDiscounted { get; set; } = false;
        public float? DiscountedTotal { get; set; }
        public float? DiscountPercentage { get; set; }
        public float AmountDue { get; set; } = 0;
        public float PaidAmount { get; set; } = 0;
        public float Balance { get; set; } = 0;

    }
}