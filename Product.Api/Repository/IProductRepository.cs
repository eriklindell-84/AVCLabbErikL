using Product.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Repository
{
    interface IProductRepository
    {
        List<Products> GetProducts();
    }
}
