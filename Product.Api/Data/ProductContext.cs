using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Api.Models;

namespace Product.Api.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Products> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = ProductService; Trusted_Connection = True;");

            
        }
    }
}



