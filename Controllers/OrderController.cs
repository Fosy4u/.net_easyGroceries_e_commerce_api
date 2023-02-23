using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DataModel;
using easyGroceries_e_commerce_api.Model;
using easyGroceries_e_commerce_api.DatabaseContext;
using System.Diagnostics;
using Newtonsoft.Json;


namespace easyGroceries_e_commerce_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        
        private readonly EcommerceDbContext _context;
        public OrderController(EcommerceDbContext context)
        {
            _context = context;
        }

        // get all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDataModel>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            List <OrderResponseModel> allOrders = new List<OrderResponseModel>();
            if(orders.Count > 0)
            {
                foreach (var order in orders)
                {   var cart = new List<CartProductModel>();
                     if(order.Cart != null)
                    {
                        cart = JsonConvert.DeserializeObject<List<CartProductModel>>(order.Cart);
                    }
               // format order for response
                    var formattedOrder = new FormattedOrderModel
                    {
                        Id = order.Id,
                        ReceiptNo = order.ReceiptNo,
                        OrderRegNo = order.OrderRegNo,
                        Cart = cart ?? new List<CartProductModel>(), 
                        Total = order.Total,
                        IsDiscounted = order.IsDiscounted,
                        DiscountedTotal = order.DiscountedTotal,
                        DiscountPercentage = order.DiscountPercentage,
                        AmountDue = order.AmountDue,
                        CreatedAt = order.CreatedAt,
                        UpdatedAt = order.UpdatedAt,
                        ShippedAt = order.ShippedAt,
                        Status = order.Status,
                        DeliveredAt = order.DeliveredAt,
                        CanceledAt = order.CanceledAt,
                        PaymentStatus = order.PaymentStatus,
                        
                        
                        
                    };
                   var customer = await _context.Customers.FindAsync(order.CustomerId);
                   var shipment = await _context.Shipments.SingleOrDefaultAsync(s => s.OrderId == order.Id);
                   var receipt = await _context.Receipts.FindAsync(order.PaymentReceiptId);
                     var response = new OrderResponseModel
                     {
                          Order = formattedOrder,
                          Customer = customer,
                          Receipt = receipt,
                          Shipment = shipment
                     };
                        allOrders.Add(response);
                    
                }   
                return Ok(allOrders); 
            }
            return Ok(orders);
            


           
        }
        // get single order
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDataModel>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound( "Order not found");
            }
            var cart = new List<CartProductModel>();
                     if(order.Cart != null)
                    {
                        cart = JsonConvert.DeserializeObject<List<CartProductModel>>(order.Cart);
                    }
            // format order for response
            var formattedOrder = new FormattedOrderModel
            {
                Id = order.Id,
                ReceiptNo = order.ReceiptNo,
                OrderRegNo = order.OrderRegNo,
                Cart = cart ?? new List<CartProductModel>(),
                Total = order.Total,
                IsDiscounted = order.IsDiscounted,
                DiscountedTotal = order.DiscountedTotal,
                DiscountPercentage = order.DiscountPercentage,
                AmountDue = order.AmountDue,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                ShippedAt = order.ShippedAt,
                Status = order.Status,
                DeliveredAt = order.DeliveredAt,
                CanceledAt = order.CanceledAt,
                PaymentStatus = order.PaymentStatus,
               
            };
            var customer = await _context.Customers.FindAsync(order.CustomerId);
            var receipt = await _context.Receipts.FindAsync(order.PaymentReceiptId);
            var shipment = await _context.Shipments.SingleOrDefaultAsync(s => s.OrderId == order.Id);
            var response = new OrderResponseModel
            {
                Order = formattedOrder,
                Customer = customer,
                Receipt = receipt,
                Shipment = shipment
            };
            


            return Ok(response);
        }
        // create order
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
        {
            if(order.CustomerId == Guid.Empty) 
            return BadRequest("Invalid order, please check missing required fields");
            var customerProfile = await _context.Customers.FindAsync(order.CustomerId);
            if (customerProfile == null)
            { 
            

                return NotFound("Customer not found"); 
             
            }
            //check if customer has pending order
            var pendingOrder = await _context.Orders.SingleOrDefaultAsync(o => o.CustomerId == order.CustomerId && o.Status == "Pending");
            if (pendingOrder != null)
            {
                return BadRequest("You have a pending order in the basket. Please complete the order or cancel it before placing a new order");
            }
            // check if customer has a royalty membership and calculate discount
            // update cart with discount if customer has royalty membership
            // calculate total
            // calculate amount due
            // update order with total, amount due, discount percentage, is discounted
            var IsRoyaltyMembership = customerProfile.IsRoyaltyMembership;
            var DiscountPercentage = IsRoyaltyMembership ? 20 : 0;
            var cart = order.Cart;
            if (cart == null || cart.Length == 0)
            {
                return BadRequest("Cart is empty");
            }
            var total = 0f;
            var updatedCart = new List<CartProductModel>();
            foreach (var item in cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return NotFound("One or more products not found");
                }
                if (product.StockQuantity < item.Quantity)
                {
                    return BadRequest("One or more products' quantity is more than stock");
                }
                total += product.Price * item.Quantity;
                  updatedCart.Add(new CartProductModel
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        UnitPrice = product.Price,
                        Quantity = item.Quantity,
                        TotalPrice = item.Quantity * product.Price,
                        IsDiscounted = DiscountPercentage > 0,
                        DiscountedPrice = IsRoyaltyMembership ? (item.Quantity * product.Price) * DiscountPercentage / 100 : null,
                        DiscountPercentage = DiscountPercentage,
                        Size = item.Size,
                        Color = item.Color,

                    });
            
            }

            var amountDue = 0f;
            if (DiscountPercentage > 0)
            {
                amountDue = total - (total * DiscountPercentage / 100);
            }
            else
            {
                amountDue = total;
            }

         
            var orderDataModel = new OrderDataModel
            {
        
               
                OrderRegNo = Guid.NewGuid(),
                Cart =  updatedCart != null ? JsonConvert.SerializeObject(updatedCart) : null,
                CustomerId = order.CustomerId,
                Status = "Pending",
                Total = total,
                AmountDue = amountDue,
                IsDiscounted = DiscountPercentage > 0,
                DiscountedTotal  = IsRoyaltyMembership ? total * DiscountPercentage / 100 : null,
                DiscountPercentage = DiscountPercentage,
                // PaymentReceiptId = null,
             

               
            };
            _context.Orders.Add(orderDataModel);
            await _context.SaveChangesAsync();
          
          // format order for response
           var formattedOrder = new FormattedOrderModel
            {
                Id = orderDataModel.Id,
                ReceiptNo = orderDataModel.ReceiptNo,
                OrderRegNo = orderDataModel.OrderRegNo,
                Cart = updatedCart ?? new List<CartProductModel>(),
                Total = orderDataModel.Total,
                IsDiscounted = orderDataModel.IsDiscounted,
                DiscountedTotal = orderDataModel.DiscountedTotal,
                DiscountPercentage = orderDataModel.DiscountPercentage,
                AmountDue = orderDataModel.AmountDue,
                CreatedAt = orderDataModel.CreatedAt,
                UpdatedAt = orderDataModel.UpdatedAt,
                ShippedAt = orderDataModel.ShippedAt,
                Status = orderDataModel.Status,
                DeliveredAt = orderDataModel.DeliveredAt,
                CanceledAt = orderDataModel.CanceledAt,
                PaymentStatus = orderDataModel.PaymentStatus,
             
                
            };
            var orderResponse = new OrderResponseModel {
                Customer = customerProfile,
                Order = formattedOrder,
                Receipt = null,
                Shipment = null
            };
            
            return Ok(orderResponse);
            
         }

         // update order

           [HttpPost("payment/{id}")]
        public async Task<ActionResult<PaymentModel>> Payment(PaymentModel payment)
        {
         // check if payment method and order id are provided
            if( payment.PaymentMethod == null || payment.OrderId == 0 ) 
            return BadRequest("Invalid order, please check if payment method and order id are provided");
            var order = await _context.Orders.FindAsync(payment.OrderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }
             var customerProfile = await _context.Customers.FindAsync(order.CustomerId);

             // check if customer exists
            if (customerProfile == null)
            { 

                return NotFound("Customer not found"); 
             
            }
            // check if order is null
            // check if order has already been paid
            if(order == null)
            {
                return NotFound("Order not found");
            }
            if(order.PaymentStatus == "Paid")
            {
                return BadRequest("Order has already been paid");
            }

            if (payment.PaidAmount < order.AmountDue)
            {
                Debug.WriteLine("Insufficient payment", order);
                return BadRequest("Insufficient payment");
            }
        var cart = new List<CartProductModel>();
        if(order.Cart != null)
         {
            cart = JsonConvert.DeserializeObject<List<CartProductModel>>(order.Cart);
         }
         if (cart == null || cart.Count == 0)
            {
                return BadRequest("Cart is empty");
            }
        
// update stock quantity for each product in cart after payment
        foreach (var item in cart)
        {
            var product = await _context.Products.FindAsync(item.Product.Id);
            if (product == null)
            {
                return NotFound("One or more products not found");
            }

            var quantity = item.Quantity;
            product.StockQuantity -= quantity;
            
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
           
            
        }
        // get fields for receipt and shipment
            var transactionReference =  Guid.NewGuid();
            var balance = payment.PaidAmount - order.AmountDue;
            Random random = new Random();
            var receiptDataModel = new ReceiptDataModel
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Customer = customerProfile,
                TransactionReference = transactionReference,
                PaymentMethod = payment.PaymentMethod,
                Total = order.Total,
                PaidAmount = payment.PaidAmount,
                AmountDue = order.AmountDue,
                IsDiscounted = order.IsDiscounted,
                DiscountedTotal = order.DiscountedTotal,
                DiscountPercentage = order.DiscountPercentage,
                ReceiptNo = random.Next(100000, 999999).ToString(),
                PaymentStatus = "Paid",
                Balance = balance,
                CreatedAt = DateTime.Now,
            };
            _context.Receipts.Add(receiptDataModel);
            await _context.SaveChangesAsync();
         
        var generatedReceipt =  await _context.Receipts.FindAsync(receiptDataModel.Id);
        if(generatedReceipt == null){
            return NotFound("Error processing payment");
        }
         var shipmentDataModel = new ShipmentDataModel
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ShipmentNo = random.Next(100000, 999999).ToString(),
            TransactionReference = transactionReference,
            ShipmentMethod = payment.ShipmentMethod,
            ShipmentCompany = "Royal Mail",
            ShipmentStatus = "Shipped",
            Address = payment.Address,
            City = payment.City,
            Country = payment.Country,
            PostCode = payment.PostCode,
            FirstName = customerProfile.FirstName,
            LastName = customerProfile.LastName,
            Email = payment.Email,

            ShipmentNotes = payment.ShipmentNotes,
            ShipmentTrackingUrl = $"https://www.royalmail.com/track-your-item/{transactionReference}"
           
        };
        _context.Shipments.Add(shipmentDataModel);
        await _context.SaveChangesAsync();
     
        //set oder status to processing since payment has been made
        order.Status = "Processing";
        order.PaymentReceiptId = generatedReceipt.Id;
        order.ReceiptNo = generatedReceipt.ReceiptNo;
        order.PaymentStatus = generatedReceipt.PaymentStatus;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
 
 // format order data
       var formattedOrder = new FormattedOrderModel
            {
                Id = order.Id,
                ReceiptNo = order.ReceiptNo,
                OrderRegNo = order.OrderRegNo,
                Cart = cart ?? new List<CartProductModel>(),
                Total = order.Total,
                IsDiscounted = order.IsDiscounted,
                DiscountedTotal = order.DiscountedTotal,
                DiscountPercentage = order.DiscountPercentage,
                AmountDue = order.AmountDue,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                ShippedAt = order.ShippedAt,
                Status = order.Status,
                DeliveredAt = order.DeliveredAt,
                CanceledAt = order.CanceledAt,
                PaymentStatus = order.PaymentStatus,
             
                
            };
        var paymentResponse = new PaymentResponseModel
        {
        
            Order = formattedOrder,
            Receipt = generatedReceipt,
            Shipment = shipmentDataModel       
        };
   
            return Ok(paymentResponse);

     
        }
    // update order
        [HttpPut("{id}")]  
        public async Task<IActionResult> PutOrder(int id, OrderModel order)

        {
            // make sure order exists
            var orderDataModel = await _context.Orders.FindAsync(id);
            if (orderDataModel == null)
            {
                return NotFound();
            }
            // validate cart is not empty
            if(order.Cart == null)
            {
                return BadRequest("Cart is empty");
            }
      // check if customer exists
            var customerProfile = await _context.Customers.FindAsync(orderDataModel.CustomerId);
            if (customerProfile == null)
            { 
            

                return NotFound("Customer not found"); 
             
            }
            
           // check if customer is a royalty member and apply discount
            var IsRoyaltyMembership = customerProfile.IsRoyaltyMembership;
            var DiscountPercentage = IsRoyaltyMembership ? 20 : 0;
            var cart = order.Cart;
            if (cart == null || cart.Length == 0)
            {
                return BadRequest("Cart is empty");
            }
            var total = 0f;
            var updatedCart = new List<CartProductModel>();
            foreach (var item in cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return NotFound("One or more products not found");
                }
                if (product.StockQuantity < item.Quantity)
                {
                    return BadRequest("One or more products' quantity is more than stock");
                }
                total += product.Price * item.Quantity;
                  updatedCart.Add(new CartProductModel
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        UnitPrice = product.Price,
                        Quantity = item.Quantity,
                        TotalPrice = item.Quantity * product.Price,
                        IsDiscounted = DiscountPercentage > 0,
                        DiscountedPrice = IsRoyaltyMembership ? (item.Quantity * product.Price) * DiscountPercentage / 100 : null,
                        DiscountPercentage = DiscountPercentage,
                        Size = item.Size,
                        Color = item.Color,

                    });
            
            }

            var amountDue = 0f;
            if (DiscountPercentage > 0)
            {
                amountDue = total - (total * DiscountPercentage / 100);
            }
            else
            {
                amountDue = total;
            }
  
               orderDataModel.OrderRegNo = Guid.NewGuid();
                orderDataModel.Cart =  updatedCart != null ? JsonConvert.SerializeObject(updatedCart) : null;
                orderDataModel.CustomerId = order.CustomerId;
                orderDataModel.Status = "Pending";
                orderDataModel.Total = total;
                orderDataModel.AmountDue = amountDue;
                orderDataModel.IsDiscounted = DiscountPercentage > 0;
                orderDataModel.DiscountedTotal  = IsRoyaltyMembership ? total * DiscountPercentage / 100 : null;
                orderDataModel.DiscountPercentage = DiscountPercentage;
                // PaymentReceiptId = null,
             

               
           
             _context.Orders.Update(orderDataModel);
            await _context.SaveChangesAsync();

            // format order data for response
          
           var formattedOrder = new FormattedOrderModel
            {
                Id = orderDataModel.Id,
                ReceiptNo = orderDataModel.ReceiptNo,
                OrderRegNo = orderDataModel.OrderRegNo,
                Cart = updatedCart ?? new List<CartProductModel>(),
                Total = orderDataModel.Total,
                IsDiscounted = orderDataModel.IsDiscounted,
                DiscountedTotal = orderDataModel.DiscountedTotal,
                DiscountPercentage = orderDataModel.DiscountPercentage,
                AmountDue = orderDataModel.AmountDue,
                CreatedAt = orderDataModel.CreatedAt,
                UpdatedAt = orderDataModel.UpdatedAt,
                ShippedAt = orderDataModel.ShippedAt,
                Status = orderDataModel.Status,
                DeliveredAt = orderDataModel.DeliveredAt,
                CanceledAt = orderDataModel.CanceledAt,
                PaymentStatus = orderDataModel.PaymentStatus,
             
                
            };
            var orderResponse = new OrderResponseModel {
                Customer = customerProfile,
                Order = formattedOrder,
                Receipt = null,
                Shipment = null
            };
            
            return Ok(orderResponse);
            
           
        } 
        // cancel order
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = "Canceled";
            order.CanceledAt = DateTime.Now;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // delete order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
    }
}