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
        public List<Product> productList = new List<Product>();
        public static List<Product> cartList = new List<Product>();
        public static double totalAmount;
        //public List<CartModel> cart = new List<CartModel>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product prod1 = new Product()
                { Id = 1, Name = "Photo1", Description = "beatufil picture by:", Price = 299.95, ImgUrl = "https://i.picsum.photos/id/145/200/300.jpg" };
                Product prod2 = new Product()
                { Id = 2, Name = "Photo2", Description = "beatufil picture by:", Price = 249.95, ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg" };
                Product prod3 = new Product()
                { Id = 3, Name = "Photo3", Description = "beatufil picture by:", Price = 179.95, ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg" };
                Product prod4 = new Product()
                { Id = 4, Name = "Photo4", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg" };
                Product prod5 = new Product()
                { Id = 5, Name = "Photo5", Description = "beatufil picture by:", Price = 129.95, ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg" };
                Product prod6 = new Product()
                { Id = 6, Name = "Photo6", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg" };
                Product prod7 = new Product()
                { Id = 7, Name = "Photo7", Description = "beatufil picture by:", Price = 129.95, ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg" };
                Product prod8 = new Product()
                { Id = 8, Name = "Photo8", Description = "beatufil picture by:", Price = 349.95, ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg" };




                productList.Add(prod1);
                productList.Add(prod2);
                productList.Add(prod3);
                productList.Add(prod4);
                productList.Add(prod5);
                productList.Add(prod6);
                productList.Add(prod7);
                productList.Add(prod8);


                var prodList = from e in productList
                               select e;

                return View("Index", prodList);
            }
        }

        public IActionResult Buy(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (product.Id != 0)
                {
                    cartList.Add(new Product() { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, ImgUrl = product.ImgUrl });

                    var cart = from e in cartList
                               .Where(e => e.Id != null).ToList()
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

        [HttpPost]
        public IActionResult Remove(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var cart = from e in cartList
                               .Where(e => e.Id != null).ToList()
                           select e;

                var RemoveFromCart = cartList.Single(r => r.Id == product.Id);
                cartList.Remove(RemoveFromCart);

                //var newTotalAmount = from e in cartList
                //              .Where(e => e.Id == product.Id).ToList()
                //                     select e.Price;


                double RemoveValue = cart.Where(p => p.Id == product.Id).Sum(x => x.Price);

                totalAmount -= RemoveValue;

                return View("Cart", cartList);
            }
        }

        
        public IActionResult RemoveAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cartList.Clear();
                totalAmount = 0;
                return View("Cart", cartList);
            }
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
