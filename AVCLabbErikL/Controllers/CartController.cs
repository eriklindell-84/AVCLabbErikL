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
        //public List<Product> cartList = new List<Product>();

        public IActionResult ConfirmOrder()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View("DoneOrder");
            }
        }


    }
}