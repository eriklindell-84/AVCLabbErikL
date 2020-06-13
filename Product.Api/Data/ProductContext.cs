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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Products>().HasData(new Products
            {
                Id = 1,
                Name = "Photo1",
                Description = "beatufil picture by:",
                Price = 249.5,
                ImgUrl = "https://i.picsum.photos/id/150/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 2,
                Name = "Photo2",
                Description = "beatufil picture by:",
                Price = 249.5,
                ImgUrl = "https://i.picsum.photos/id/200/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 3,
                Name = "Photo3",
                Description = "beatufil picture by:",
                Price = 179.5,
                ImgUrl = "https://i.picsum.photos/id/225/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 4,
                Name = "Photo4",
                Description = "beatufil picture by:",
                Price = 349,
                ImgUrl = "https://i.picsum.photos/id/240/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 5,
                Name = "Photo5",
                Description = "beatufil picture by:",
                Price = 129.5,
                ImgUrl = "https://i.picsum.photos/id/199/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 6,
                Name = "Photo6",
                Description = "beatufil picture by:",
                Price = 349.5,
                ImgUrl = "https://i.picsum.photos/id/175/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 7,
                Name = "Photo7",
                Description = "beatufil picture by:",
                Price = 129,
                ImgUrl = "https://i.picsum.photos/id/270/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 8,
                Name = "Photo8",
                Description = "beatufil picture by:",
                Price = 349,
                ImgUrl = "https://i.picsum.photos/id/300/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
            {
                Id = 9,
                Name = "Photo9",
                Description = "beatufil picture by:",
                Price = 99.5,
                ImgUrl = "https://i.picsum.photos/id/284/200/300.jpg"
            });
            builder.Entity<Products>().HasData(new Products
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



