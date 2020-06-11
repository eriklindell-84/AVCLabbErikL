using Product.Api.Models;
using Product.Api.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Product.Api.Test
{
    public class ProgramApiTests
    {
        [Fact]
        public void GetOneProductById_Gets_One_Product_With_Id()
        {
            
            ProductRepository pr = new ProductRepository();
            var product = pr.GetProductById(3);

            Assert.Equal(product.Id, 3);
        }

        [Fact]
        public void GetProducs_Gets_a_typOf_Product()
        { 
            ProductRepository pr = new ProductRepository();
            var product = pr.GetProducts();

            Assert.IsType<List<Products>>(product);
        }

        [Fact]
        public void GetProducs_Lenght_Equals_List_Of_Products_Lengt()
        {
            ProductRepository pr = new ProductRepository();
            var product = pr.GetProducts();
            var sizeofList = product.Count;

            Assert.Equal(sizeofList, product.Count);
        }

    }
}
