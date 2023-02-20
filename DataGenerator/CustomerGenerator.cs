using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DatabaseContext;
using easyGroceries_e_commerce_api.DataModel;

namespace easyGroceries_e_commerce_api.DataGenerator
{
    public class CustomerGenerator
    {
        public static void Initialize (IServiceProvider serviceProvider) {

            using (var context = new EcommerceDbContext(serviceProvider.GetRequiredService<DbContextOptions<EcommerceDbContext>>()))
            {
                // Look for any customers.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customers.AddRange(
                    new CustomerDataModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Kesh",
                        Email = "John.kesh@yahoo.com",
                        PostCode = "SW2 1AA",
                        City = "London",
                        Country = "UK",
                        Address = "1, London Street",
                        Phone = "0123456789"
                      
                      
                    },
                    new CustomerDataModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Mathew",
                        LastName = "Las",
                        Email = "Mathew.Lass@yahoo.com",
                        PostCode = "SE1 1AA",
                        City = "London",
                        Country = "UK",
                        Address = "2, London Street",
                        Phone = "01234345789"
                     
                    },
                    new CustomerDataModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Foster",
                        LastName = "Mason",
                        Email = "Foster.Mason@yahoo.com",
                        PostCode = "W1 1AA",
                        City = "London",
                        Country = "UK",
                        Address = "3, London Street",
                        Phone = "01224656789"
                      
                    },
                    new CustomerDataModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Mike",
                        LastName = "Kilo",
                        Email = "Mike.Kilo@gmail.com",
                        PostCode = "W2 1AA",
                        City = "London",
                        Country = "UK",
                        Address = "4, London Street",
                        Phone = "0123986782",
                      
                    },
                    new CustomerDataModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Rita",
                        LastName = "Kuti",
                        Email = "Rita.Kuti@gmail.com",
                      PostCode = "EC 1AA",
                        City = "London",
                        Country = "UK",
                        Address = "4, London Street",
                        Phone = "07076556655",
                    
                    }
               

                );
                context.SaveChanges();

            }



        }

        
    }
}