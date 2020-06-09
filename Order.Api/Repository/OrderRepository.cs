using Order.Api.Data;
using Order.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderContext orderContext = new OrderContext();
        public List<Orders> GetOrders()
        {
            var orders = orderContext.Orders.ToList();
            return orders;
        }
        public Orders GetOrderById(int Id)
        {
            var order = orderContext.Orders.Where(p => p.Id == Id).FirstOrDefault();
            return order;
        }
        public void PlaceOrder(Orders order)
        {
            orderContext.Add(order);
            orderContext.SaveChanges();
            
        }
    }
}
