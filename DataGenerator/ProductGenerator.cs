
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
                var colors = new List<string> {"Black", "White", "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "Brown", "Grey", "Beige", "Gold", "Silver", "Multi"};
                 

                context.Products.AddRange(
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "SAND FAUX SUEDE POINTED SIDE SEAM DETAIL WEDGE HEELED KNEE BOOTS",
                        Description = "Take your shoe-drobe to new heights with these sand faux suede pointed side seam detail wedge-heeled knee boots. Their sand faux suede material is so on-trend this season whilst its seam detail adds some chic vibes to your look. These knee-high boots have a wedge heel and pointed design which is so fresh this season. Pair these boots with your weekend-worthy 'fit for a look that is sure to impress.",
                        Price = 50,
                        ImageUrl = "https://cdn-img.prettylittlething.com/4/2/b/4/42b4e70f836bec2a556ab457cd39a7da21437f77_cnc3363_3.jpg?imwidth=1024",
                        Category = "Shoes",
                        ProductBrand = "Pretty Little Thing",
                        StockQuantity = 10,
                        Colors = JsonConvert.SerializeObject(new List<string> {"Black", "White", "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "Brown"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M", "L", "XL", "XXL"}),
                        
                     

                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "STRUCTURED CONTOUR RIBBED LEGGINGS",
                        Description = "A simple pair of contour leggings are the ultimate staple piece for your new season wardrobe and these are sure to be at the top of your new season hit list. Featuring a black ribbed material in a figure-skimming structured and a contour fit, what's not to love? Ribbed leggings are the perfect piece to bring you endless chic looks. To complete the look just team these high waisted leggings with the matching crop top and fresh kicks.",
                        Price = 15,
                        ImageUrl = "https://st4.depositphotos.com/14953852/22772/v/600/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg",
                        Category = "Clothing",
                        StockQuantity = 20,
                        ProductBrand = "Preety Little Thing",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Grey", "Beige", "Gold", "Silver", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",   "XXL"}),

                     
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "ORANGE SHELL POCKET WIDE LEG CARGO TROUSERS",
                        Description = "Your look has never been so on-trend with this orange shell pocket wide leg cargo trousers. Made in an orange shell material with pocket detailing, a wide leg fit and a cargo style, you're sure to have all eyes on you doll. Pair these women's cargo trousers with the matching corset, high heels and statement accessories for a look like no other.",
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
                        Name = "SLIM FIT LONG SLEEVE T-SHIRT",
                        Description = "Slim fit long sleeve T-shirt with a round neckline.",
                        Price = 20,
                        ImageUrl = "https://static.zara.net/photos///2023/V/0/2/p/5584/463/779/82/w/1126/5584463779_1_1_1.jpg?ts=1676282086407",
                        Category = "Clothing",
                        StockQuantity = 30,
                        ProductBrand = "Zara",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Black", "White",  "Grey", "Purple", "Gold", "Silver", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",  "L", "XL", "XXL"}),
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "TRAINERS X RHUIGI",
                        Description = "Trainers with a combination of pieces on the upper. Eight-eyelet facing. Sole with a combination of colour and irregular design. Available in several colours. Model inspired by the sport of Futsal.",
                        Price = 400,
                        ImageUrl = "https://static.zara.net/photos///2023/V/1/2/p/2220/120/800/2/w/1126/2220120800_2_3_1.jpg?ts=1676546031893",
                        Category = "Shoes",
                        ProductBrand = "Zara",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Black", "White",  "Green", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {  "L", "XL", "XXL"}),
                    },
                    new ProductDataModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product 5",
                        Description = "Product 5 Description",
                        Price = 200,
                        ImageUrl = "https://cdn-img.prettylittlething.com/6/8/7/2/68727fa8245d702d24b22bc1e85f92c2e014c80c_cmy3934_3.jpg?imwidth=1024",
                        Category = "Shoes",
                        StockQuantity = 50,
                        ProductBrand = "Pretty Little Thing",
                        Colors = JsonConvert.SerializeObject(new List<string> {"Orange", "White",  "Purple", "Grey", "Beige"}),
                        Sizes = JsonConvert.SerializeObject(new List<string> {"S", "M",  "XXL"}),
                    }
                );
                context.SaveChanges();

            
        }
    }
}
}
