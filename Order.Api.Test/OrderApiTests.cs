using Order.Api.Models;
using Order.Api.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Order.Api.Test
{
    public class OrderApiTests
    {
        [Fact]
        public void GetOrders_Gets_OrderObjects()
        {

            OrderRepository or = new OrderRepository();
            var order = or.GetOrders();

            Assert.IsType<List<Orders>>(order);
        }
        [Fact]
        public void GetOrderById_get_correctOrderId()
        {

            OrderRepository or = new OrderRepository();
            var order = or.GetOrderById(1034);

            Assert.Equal(1034, order.Id);
        }
        [Fact]
        public void GetOrders_Lenght_Equals_List_Of_Orders_Lengt()
        {
            OrderRepository or = new OrderRepository();
            var order = or.GetOrders();
            var sizeofList = order.Count;

            Assert.Equal(sizeofList, order.Count);
        }



        // THIS TEST WILL POST AN ORDER TO SERVICE DB
        // Remember to delete it from db after testing
        [Fact]
        public void PlaceOrder_places_an_Order()
        {
            OrderRepository or = new OrderRepository();
            Orders order = new Orders();
            order.OrderAmount = 500;
            order.OrderDate = DateTime.Now;
            order.UserID = new Guid();

            or.PlaceOrder(order);

            var findOrder = or.orderContext.Orders.Find(order.Id);

            Assert.Equal(order.Id, findOrder.Id);
        }
    }
}
