
using Microsoft.EntityFrameworkCore;
using easyGroceries_e_commerce_api.DatabaseContext;
using easyGroceries_e_commerce_api.DataModel;
using Newtonsoft.Json;

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
                var colors = new List<string> { "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "Brown", "Grey", "Beige", "Gold", "Silver", "Multi"};
                 

                context.Products.AddRange(
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "HEELED KNEE BOOTS",
                        Description = "Take your shoe-drobe to new heights with these sand faux suede pointed side seam detail wedge-heeled knee boots.",
                        Price = 50,
                        ImageUrl = "https://cdn-img.prettylittlething.com/4/2/b/4/42b4e70f836bec2a556ab457cd39a7da21437f77_cnc3363_3.jpg?imwidth=1024",
                        Category = "Shoes",
                        ProductBrand = "Pretty Little Thing",
                        StockQuantity = 10,
                        Colors = JsonConvert.SerializeObject(new List<string> { "Yellow", "Orange", "Purple", "Pink", "Brown"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M", "L", "XL", "XXL"}),
                        
                     

                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "SLIM RIBBED LEGGINGS",
                        Description = "A simple pair of contour leggings are the ultimate staple piece for your new season wardrobe and these are sure to be at the top of your new season hit list.",
                        Price = 15,
                        ImageUrl = "https://cdn-img.prettylittlething.com/4/4/9/2/4492e89c0510eb9339a1115c7b38abf02627cc59_cmo6525_5.jpg?imwidth=1024",
                        Category = "Clothing",
                        StockQuantity = 20,
                        ProductBrand = "Preety Little Thing",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Red", "Gold", "Silver", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",   "XXL"}),

                     
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "ORANGE CARGO TROUSERS",
                        Description = "Your look has never been so on-trend with this orange shell pocket wide leg cargo trousers.",
                        Price = 28,
                        ImageUrl = "https://cdn-img.prettylittlething.com/4/e/a/c/4eac846c6bc540ed16de02455e93b30726316c0d_cnd0641_1.jpg?imwidth=1024",
                        Category = "Clothing",
                        StockQuantity = 20,
                        ProductBrand = "Preety Little Thing",
                        Colors = JsonConvert.SerializeObject(new List<string> { "Orange"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",  "L", "XL"}),
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "LONG SLEEVE T-SHIRT",
                        Description = "Slim fit long sleeve T-shirt with a round neckline.",
                        Price = 20,
                        ImageUrl = "https://static.zara.net/photos///2023/V/0/2/p/5584/463/779/82/w/1126/5584463779_1_1_1.jpg?ts=1676282086407",
                        Category = "Clothing",
                        StockQuantity = 30,
                        ProductBrand = "Zara",
                        Colors = JsonConvert.SerializeObject(new List<string> {  "Grey", "Purple", "Gold", "Silver", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",  "L", "XL", "XXL"}),
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "TRAINERS X RHUIGI",
                        Description = "Trainers with a combination of pieces on the upper.  Sole with a combination of colour and irregular design.",
                        Price = 400,
                        ImageUrl = "https://static.zara.net/photos///2023/V/1/2/p/2220/120/800/2/w/1126/2220120800_2_3_1.jpg?ts=1676546031893",
                        Category = "Shoes",
                        StockQuantity = 5,
                        ProductBrand = "Zara",
                        Colors = JsonConvert.SerializeObject(new List<string> {   "Green", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {  "L", "XL", "XXL"}),
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "SANDAL WITH STRAPS",
                        Description = "Perfect for the summer, these sandals are a must-have for your shoe-drobe.",
                        Price = 200,
                        ImageUrl = "https://cdn-img.prettylittlething.com/6/8/7/2/68727fa8245d702d24b22bc1e85f92c2e014c80c_cmy3934_3.jpg?imwidth=1024",
                        Category = "Shoes",
                        StockQuantity = 50,
                        ProductBrand = "Pretty Little Thing",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Orange",   "Purple", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",  "XXL"}),
                    }
                );
                context.SaveChanges();

            
        }
    }
}
}
