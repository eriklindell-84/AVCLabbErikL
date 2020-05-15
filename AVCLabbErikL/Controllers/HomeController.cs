using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AVCLabbErikL.Models;
using AVCLabbErikL.Data;

namespace AVCLabbErikL.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        // List of the products
        public List<ProductModel> productList = new List<ProductModel>();

        // List of the items in shopping cart
        public static List<ProductModel> cartList = new List<ProductModel>();

        // varibale to store total amount to pay
        public static double totalAmount;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Mockdata  for adding products
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ProductModel prod1 = new ProductModel()
                { Id = 1, Name = "Photo1", Description = "beatufil picture by:", Price = 299.95, ImgUrl = "https://i.picsum.photos/id/145/200/300.jpg" };
                ProductModel prod2 = new ProductModel()
                { Id = 2, Name = "Photo2", Description = "beatufil picture by:", Price = 249.95, ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg" };
                ProductModel prod3 = new ProductModel()
                { Id = 3, Name = "Photo3", Description = "beatufil picture by:", Price = 179.95, ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg" };
                ProductModel prod4 = new ProductModel()
                { Id = 4, Name = "Photo4", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg" };
                ProductModel prod5 = new ProductModel()
                { Id = 5, Name = "Photo5", Description = "beatufil picture by:", Price = 129.95, ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg" };
                ProductModel prod6 = new ProductModel()
                { Id = 6, Name = "Photo6", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg" };
                ProductModel prod7 = new ProductModel()
                { Id = 7, Name = "Photo7", Description = "beatufil picture by:", Price = 129.95, ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg" };
                ProductModel prod8 = new ProductModel()
                { Id = 8, Name = "Photo8", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg" };
                ProductModel prod9 = new ProductModel()
                { Id = 9, Name = "Photo9", Description = "beatufil picture by:", Price = 99.95, ImgUrl = "https://i.picsum.photos/id/284/200/300.jpg" };
                ProductModel prod10 = new ProductModel()
                { Id = 10, Name = "Photo10", Description = "beatufil picture by:", Price = 149.95, ImgUrl = "https://i.picsum.photos/id/198/200/300.jpg" };




                // Populate the product List
                productList.Add(prod1);
                productList.Add(prod2);
                productList.Add(prod3);
                productList.Add(prod4);
                productList.Add(prod5);
                productList.Add(prod6);
                productList.Add(prod7);
                productList.Add(prod8);
                productList.Add(prod9);
                productList.Add(prod10);

                // Get content from ProductList
                var prodList = from e in productList
                               select e;

                // Show content of productList
                return View("Index", prodList);
            }
        }

        // Add Product to Cart and add its Price to TotalSum
        public IActionResult Buy(ProductModel product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (product.Id != 0)
                {
                    cartList.Add(new ProductModel() { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, ImgUrl = product.ImgUrl, Quantity = 1});

                    var cart = from e in cartList

                               select e;
                    totalAmount = 0;
                    foreach (var item in cartList)
                    {
                        totalAmount += item.Price;
                    }
                }
            }

            return View("Cart", cartList);
        }


        // Remove Product from Cart and Remove its Value from Total Sum
        [HttpPost]
        public IActionResult Remove(ProductModel product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // get cart items
                var cart = from e in cartList
                           .Where(p => p.Id == product.Id)
                           select e;

                // Get Id of product for which to remove price and romve the cost from Cart Total Price.
                double RemoveValue = cart.Where(p => p.Id == product.Id).Sum(x => x.Price);
                
                if (totalAmount > 0)
                {
                    totalAmount -= RemoveValue;
                }
                //Remove the actual product from the cart
                var RemoveFromCart = cartList.FirstOrDefault(r => r.Id == product.Id);
                cartList.Remove(RemoveFromCart);



                ////When products are removed with remove button
                //product.Quantity = 0;

                return View("Cart", cartList);
            }
        }

        // Remove All items from shopping cart and Set Total Sum to 0
        public IActionResult RemoveAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cartList.Clear();
                totalAmount = 0;
                return View("Cart", cartList);
            }
        }
        //// Adding 1 addition quantity of the product
        [HttpPost]
        public IActionResult AddOne(ProductModel product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var productQuantity = from p in cartList
                                      .Where(p => p.Id == product.Id)
                                      select p.Quantity;

                //product.Quantity = Convert.ToInt32(productQuantity);

                product.Quantity++;

                double AddValue = cartList.Where(p => p.Id == product.Id).Sum(x => x.Price);

                totalAmount += AddValue;

                return View("Cart", cartList);
            }
        }

        [HttpPost]
        // Removing 1 from quantity of the product
        public IActionResult SubtractOne(ProductModel product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var cart = from e in cartList
                               .Where(e => e.Id == product.Id)
                           select e;

                // Check if quantity of product is greater then 0, if so remove 1
                if (product.Quantity > 0)
                {
                    product.Quantity -= 1;
                }


                double RemoveValue = cartList.Where(p => p.Id == product.Id).Sum(x => x.Price);

                //check if value of Totalsum is greate then 0, then remove value
                if (totalAmount >= 0)
                {
                    totalAmount -= RemoveValue;
                }
                //Set total value if it should pass below 0
                if (totalAmount < 0)
                {
                    totalAmount = 0;
                }

                return View("Cart", cartList);
            }
        }

        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
