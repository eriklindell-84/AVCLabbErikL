using System;
using System.Collections.Generic;
using System.Text;
using AVCLabbErikL.Models;
using AVCLabbErikL.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AVCLabbErikL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private const string ConnectionString = "Server = (localdb)\\MSSQLLocalDB; Database = AvcErikL; Trusted_Connection = True;";

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
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<Adress> Adresses { get; set; }


    }
}
