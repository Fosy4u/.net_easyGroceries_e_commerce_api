using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easyGroceries_e_commerce_api.Model
{
    public class RoyaltySubscriptionModel
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; } = "";
        public int AmountPaid { get; set; } = 0;
    }
}