using Microsoft.EntityFrameworkCore;
using Order.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Data
{
    public class OrderContext : DbContext
    {
            public DbSet<Orders> Orders { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = ProductService; Trusted_Connection = True;");
            }
        }
    }

