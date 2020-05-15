using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AVCLabbErikL.Models;
using AVCLabbErikL.Data;
using Microsoft.AspNetCore.Identity;

namespace AVCLabbErikL.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // List of the products
        public List<ProductModel> productList = new List<ProductModel>();

        // List of the items in shopping cart
        public static List<ProductModel> cartList = new List<ProductModel>();

        // varibale to store total amount to pay
        public static double totalAmount;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            // Mockdata  for adding products
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ProductModel prod1 = new ProductModel()
                { Id = 1, Name = "Photo1", Description = "beatufil picture by:", Price = 299, ImgUrl = "https://i.picsum.photos/id/145/200/300.jpg" };
                ProductModel prod2 = new ProductModel()
                { Id = 2, Name = "Photo2", Description = "beatufil picture by:", Price = 249.5, ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg" };
                ProductModel prod3 = new ProductModel()
                { Id = 3, Name = "Photo3", Description = "beatufil picture by:", Price = 179.5, ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg" };
                ProductModel prod4 = new ProductModel()
                { Id = 4, Name = "Photo4", Description = "beatufil picture by:", Price = 349, ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg" };
                ProductModel prod5 = new ProductModel()
                { Id = 5, Name = "Photo5", Description = "beatufil picture by:", Price = 129.5, ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg" };
                ProductModel prod6 = new ProductModel()
                { Id = 6, Name = "Photo6", Description = "beatufil picture by:", Price = 349.5, ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg" };
                ProductModel prod7 = new ProductModel()
                { Id = 7, Name = "Photo7", Description = "beatufil picture by:", Price = 129, ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg" };
                ProductModel prod8 = new ProductModel()
                { Id = 8, Name = "Photo8", Description = "beatufil picture by:", Price = 349, ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg" };
                ProductModel prod9 = new ProductModel()
                { Id = 9, Name = "Photo9", Description = "beatufil picture by:", Price = 99.5, ImgUrl = "https://i.picsum.photos/id/284/200/300.jpg" };
                ProductModel prod10 = new ProductModel()
                { Id = 10, Name = "Photo10", Description = "beatufil picture by:", Price = 149, ImgUrl = "https://i.picsum.photos/id/198/200/300.jpg" };




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
        public IActionResult Buy(ProductModel product, double total)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (product.Id != 0)
                {
                    cartList.Add(new ProductModel() { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, ImgUrl = product.ImgUrl, Quantity = 1});
                    totalAmount = total;
                    var cart = from e in cartList
                               select e;
                    
                    foreach (var item in cart)
                    {
                        totalAmount += item.Price * item.Quantity;
                       
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

                //Get the specific product by id
                var cart = from e in cartList
                               .Where(e => e.Id == product.Id)
                           select e;
                
                // Remove 1x price per quantity in bag
                foreach (var item in cart)
                {
                    totalAmount -= item.Price * item.Quantity;
                }

                // Get Id of product for which to remove price and romve the cost from Cart Total Price.
                double RemoveValue = cart.Where(p => p.Id == product.Id).Sum(x => x.Price);

                //Remove the actual product from the cart
                var RemoveFromCart = cartList.FirstOrDefault(r => r.Id == product.Id);
                cartList.Remove(RemoveFromCart);

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
                //Get item Quantity and Add one to it for this product id
                var cart = from e in cartList
                               .Where(e => e.Id == product.Id)
                           select e;

                foreach (var item in cart)
                {
                    item.Quantity += 1;
                }
                

                // Get Value of Item Price
                double AddValue = cartList.Where(p => p.Id == product.Id).Sum(x => x.Price);

                // Add item price to Total Amount
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
                //Get the specific product by id
                var cart = from e in cartList
                               .Where(e => e.Id == product.Id)
                           select e;

                //Get the Price of the product that's rmeoved
                double RemoveValue = cart.Where(p => p.Id == product.Id).Sum(x => x.Price);

                // Check if quantity of product is greater then 0, if so remove 1
                //Only removes from total is quntity is greater then 0
                foreach (var item in cart)
                {
                    if (item.Quantity > 0)
                    {
                        item.Quantity -= 1;
                        totalAmount -= RemoveValue;
                    }
                }
                if (totalAmount < 0)
                {
                    totalAmount = 0;
                }
                return View("Cart", cartList);
            }
        }

        public IActionResult ConfirmOrder()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var cart = from e in cartList
                               select e;

                return View("DoneOrder", cart);
            }
        }

            //public IActionResult Cart()
            //{
            //    return View();
            //}
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
