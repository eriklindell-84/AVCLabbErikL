using System;
using System.Collections.Generic;
using System.Text;
using AVCLabbErikL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AVCLabbErikL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = AVCErikL; Trusted_Connection = True;");
            }
        }
        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
    }
}
