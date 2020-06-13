using System;
using System.Collections.Generic;
using System.Text;
using AVCLabbErikL.Models;
using AVCLabbErikL.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AVCLabbErikL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers>
    {
        private const string ConnectionString = "Server = (localdb)\\MSSQLLocalDB; Database = AvcErikL; Trusted_Connection = True;";

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<ApplicationUsers> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}

