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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 1,
                Name = "Photo1",
                Description = "beatufil picture by:",
                Price = 249.5,
                ImgUrl = "https://i.picsum.photos/id/150/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 2,
                Name = "Photo2",
                Description = "beatufil picture by:",
                Price = 249.5,
                ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 3,
                Name = "Photo3",
                Description = "beatufil picture by:",
                Price = 179.5,
                ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 4,
                Name = "Photo4",
                Description = "beatufil picture by:",
                Price = 349,
                ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 5,
                Name = "Photo5",
                Description = "beatufil picture by:",
                Price = 129.5,
                ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 6,
                Name = "Photo6",
                Description = "beatufil picture by:",
                Price = 349.5,
                ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 7,
                Name = "Photo7",
                Description = "beatufil picture by:",
                Price = 129,
                ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 8,
                Name = "Photo8",
                Description = "beatufil picture by:",
                Price = 349,
                ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 9,
                Name = "Photo9",
                Description = "beatufil picture by:",
                Price = 99.5,
                ImgUrl = "https://i.picsum.photos/id/284/200/300.jpg"
            });
            builder.Entity<ProductModel>().HasData(new ProductModel
            {
                Id = 10,
                Name = "Photo10",
                Description = "beatufil picture by:",
                Price = 149,
                ImgUrl = "https://i.picsum.photos/id/198/200/300.jpg"
            });


        }
    }
}

