using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Repository;

namespace Product.Api.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductRepository pr = new ProductRepository();
        
        //Get all Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productlist = pr.GetProducts();
            return Ok(productlist);
        }
        //Get Product By Id
        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var productlist = pr.GetProductById(Id);
            return Ok(productlist);
        }
    }
}