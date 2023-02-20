using System.ComponentModel.DataAnnotations;

namespace easyGroceries_e_commerce_api.Model
{
    public class CustomerModel
    {
      
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsRoyaltyMembership { get; set; } = false;
        
    }
}