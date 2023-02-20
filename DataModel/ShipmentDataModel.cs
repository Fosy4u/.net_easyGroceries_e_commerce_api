using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.DataModel
{
    public class ShipmentDataModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public Guid TransactionReference { get; set; }
        public string? ShipmentStatus { get; set; } = "pending";
        public string? ShipmentMethod { get; set; } = "standard";
        public string? ShipmentTrackingNo { get; set; }
        public string? ShipmentTrackingUrl { get; set; }
        public string? ShipmentNotes { get; set; }
        public string? ShipmentNo { get; set; }
        public string? ShipmentCompany { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }  = DateTime.Now;
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string PostCode { get; set; } = "";
    }
}