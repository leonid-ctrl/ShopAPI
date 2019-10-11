using ShopDbAccess.Models;
using System;
using System.Collections.Generic;

namespace ShopDbAccess.DAL
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderByID(int orderID);
        void CreateOrder(Order order);
        void DeleteOrderByID(int orderID);
        void UpdateOrder(Order order);
        void Save();
    }
}