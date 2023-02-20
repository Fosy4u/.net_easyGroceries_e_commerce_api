using System.ComponentModel.DataAnnotations;


namespace easyGroceries_e_commerce_api.Model
{
    public class PaymentModel
    {
        
        [Required]
        public int OrderId { get; set; } = 0;
        [Required]
        public string? PaymentMethod { get; set; }
        public string? ShipmentMethod { get; set; } = "standard";
        public float PaidAmount { get; set; } = 0;
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string City { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string PostCode { get; set; } = "";
        public string? ShipmentNotes { get; set; }
    }
}