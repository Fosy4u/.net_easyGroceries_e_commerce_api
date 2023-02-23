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
                        Phone = "0123456789",
                        ImageUrl = "https://images.unsplash.com/photo-1488426862026-3ee34a7d66df?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MzV8fHdvbWFufGVufDB8fDB8fA%3D%3D&w=1000&q=80"
                      
                      
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
                        Phone = "01234345789",
                        ImageUrl="https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
                     
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
                        Phone = "01224656789",
                        ImageUrl="https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MzJ8fG1hbGV8ZW58MHx8MHx8&w=1000&q=80"
                      
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
                        ImageUrl="https://taylormaleimage.co.uk/wp-content/uploads/2021/05/taylor-male-image-2.jpg"
                      
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
                        ImageUrl="https://img.freepik.com/free-photo/young-beautiful-woman-pink-warm-sweater-natural-look-smiling-portrait-isolated-long-hair_285396-896.jpg"
                    
                    }
               

                );
                context.SaveChanges();

            }



        }

        
    }
}