using Microsoft.EntityFrameworkCore;
using Product.Api.Data;
using Product.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductContext productContext = new ProductContext();
        public List<Products> GetProducts()
        {
            var products = productContext.Products.ToList();
            return products;
        }
        public Products GetProductById(int Id)
        {
            var prod = productContext.Products.Where(p=>p.Id == Id).FirstOrDefault();
            return prod;
        }
    }
}
