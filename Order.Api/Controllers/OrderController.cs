using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Data;
using Order.Api.Models;
using Order.Api.Repository;

namespace Order.Api.Controllers
{
    [Route("api/Order")]
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
        [HttpPost]
        public IActionResult PlaceOrder([FromBody]Orders order)
        {
            or.PlaceOrder(order);
            return Ok(order);
        }
    }
}