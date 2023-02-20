
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DataModel;

namespace easyGroceries_e_commerce_api.DatabaseContext
{
 

    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
               
         
             optionsBuilder.UseInMemoryDatabase(databaseName:  "EcommerceDb");
            
        }   
        

        public DbSet<ProductDataModel> Products { get; set; }
        public DbSet<CustomerDataModel> Customers { get; set; }
        public DbSet<OrderDataModel> Orders { get; set; }
        public DbSet<ReceiptDataModel> Receipts { get; set; }
        public DbSet<ShipmentDataModel> Shipments { get; set; }
        

        


    }
}

