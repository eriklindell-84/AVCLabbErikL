using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVCLabbErikL.Data;
using AVCLabbErikL.Models;
using Microsoft.AspNetCore.Mvc;
namespace AVCLabbErikL.Controllers
{
    public class CartController : Controller
    {
        public static List<ProductModel> cartList = new List<ProductModel>();

        // Add Product to Cart and add its Price to TotalSum
        public IActionResult Buy(ProductModel product, double total)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // Check if product excist in the cart already
                var ExcistInCart = cartList.Where(p => p.Id == product.Id);

                // If product don't excist add the product to cart
                if (ExcistInCart.Count() == 0)
                {
                    cartList.Add(new ProductModel(http) { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, ImgUrl = product.ImgUrl, Quantity = 1 });

                    AVCLabbErikL.Controllers.HomeController.totalAmount = total;
                    var cart = from e in cartList
                               select e;
                    // Adjust total amount in regards to how many on the item is in cart.
                    foreach (var item in cart)
                    {
                        AVCLabbErikL.Controllers.HomeController.totalAmount += item.Price * item.Quantity;

                    }

                }
                else
                {
                    // If product excist, add 1 to quantity instead óf adding another card to the shopping cart.

                    // define what item is going to be modified
                    var cart = from e in cartList
                                .Where(p => p.Id == product.Id)
                               select e;

                    // select the item and add 1 to quantity and it's price to Total Amount
                    cart.Take(1).Where(p => p.Id.Equals(product.Id));
                    foreach (var item in cart)
                    {
                        item.Quantity++;
                        AVCLabbErikL.Controllers.HomeController.totalAmount += product.Price;
                    }
                }
            }

            return View("Cart", cartList);
        }
        public IActionResult ShowCart()
        {
            if (cartList.Count != 0)
            {
                RedirectToAction("Buy");
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
                    AVCLabbErikL.Controllers.HomeController.totalAmount -= item.Price * item.Quantity;
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
                AVCLabbErikL.Controllers.HomeController.totalAmount = 0;
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
                AVCLabbErikL.Controllers.HomeController.totalAmount += AddValue;

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
                        AVCLabbErikL.Controllers.HomeController.totalAmount -= RemoveValue;
                    }
                }
                if (AVCLabbErikL.Controllers.HomeController.totalAmount < 0)
                {
                    AVCLabbErikL.Controllers.HomeController.totalAmount = 0;
                }
                return View("Cart", cartList);
            }
        }




    }
}