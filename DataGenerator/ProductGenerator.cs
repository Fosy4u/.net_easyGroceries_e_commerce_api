
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DatabaseContext;
using easyGroceries_e_commerce_api.DataModel;

namespace easyGroceries_e_commerce_api.DataGenerator
{
    public class ProductGenerator
    {
        
        public static void Initialize (IServiceProvider serviceProvider) {

            using (var context = new EcommerceDbContext(serviceProvider.GetRequiredService<DbContextOptions<EcommerceDbContext>>()))
            {
                // Look for any products.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 1",
                        Description = "Product 1 Description",
                        Price = 100,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Product Type 1",
                        ProductBrand = "Product Brand 1",
                        StockQuantity = 10
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 2",
                        Description = "Product 2 Description",
                        Price = 200,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Product Type 2",
                        StockQuantity = 20,
                        ProductBrand = "Product Brand 2"
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 3",
                        Description = "Product 3 Description",
                        Price = 300,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Product Type 3",
                        StockQuantity = 30,
                        ProductBrand = "Product Brand 3"
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 4",
                        Description = "Product 4 Description",
                        Price = 400,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Product Type 4",
                        ProductBrand = "Product Brand 4"
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 5",
                        Description = "Product 5 Description",
                        Price = 500,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Product Type 5",
                        StockQuantity = 50,
                        ProductBrand = "Product Brand 5"
                    }
                );
                context.SaveChanges();

            
        }
    }
}
}
