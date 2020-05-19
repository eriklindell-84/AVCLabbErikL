using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AVCLabbErikL.Data;
using AVCLabbErikL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AVCLabbErikL.Controllers
{
    public class OrderController : Controller
    {
        public static List<OrderModel> getOrdersList = new List<OrderModel>();
        // GET: /<controller>/

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                OrderModel om = new OrderModel();
                // Get orders for the signed in User
                var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


                var orders = from o in db.Orders
                                 .Where(o => o.UserID == Guid.Parse(signedInUserID))
                             select o;

                //Return all Orders for signed in User
                return View("GetOrders", orders.ToList());
            }
        }
        [HttpPost]
        public IActionResult ConfirmOrder()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                // Logic for creating an Order //

                // get the signed in user id
                var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                OrderModel om = new OrderModel();

                // Get the most recent order for the signed in user
                var orders = from o in db.Orders
                                 .Where(o => o.UserID == Guid.Parse(signedInUserID))
                                 .OrderByDescending(o => o.OrderDate)
                                 .Take(1)
                             select o;

                // Add item to the getOrderList(To be displayed once the order is completed)
                //foreach (var item in orders)
                //{
                //    getOrdersList.Add(item);
                //}

                // Grab all items from cartlist
                var cart = from e in AVCLabbErikL.Controllers.CartController.cartList
                           select e;


                // Set values for the Order
                foreach (var item in cart)
                {
                    Random random = new Random();
                    
                    
                    om.Id = random.Next(1, 5000);
                    om.OrderAmount = AVCLabbErikL.Controllers.HomeController.totalAmount;
                    om.OrderDate = DateTime.Now;
                    om.UserID = Guid.Parse(signedInUserID);

                }
                // Add order and save it to db
                db.Add(om);
                db.SaveChanges();

                //clear the cart after order and set totalamont to 0
                AVCLabbErikL.Controllers.CartController.cartList.Clear();
                AVCLabbErikL.Controllers.HomeController.totalAmount = 0;

                return View("ConfirmOrder", orders.ToList()/*getOrdersList*/);
            }
        }
    }
}
