using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Repository;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderRepository or = new OrderRepository();
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orderslist = or.GetOrders();
            return Ok(orderslist);
        }
        [HttpGet("{Id}")]
        public IActionResult GetOrderById(int Id)
        {
            var order = or.GetOrderById(Id);
            return Ok(order);
        }
    }
}