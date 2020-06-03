using Order.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Repository
{
    interface IOrderRepository
    {
        List<Orders> GetOrders();
    }
}
