using ShopDbAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDbAccess.DAL
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private ShopContext context;

        public OrderRepository(ShopContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders.ToList();
        }

        public Order GetOrderByID(int orderID)
        {
            return context.Orders.Find(orderID);
        }

        public void CreateOrder(Order order)
        {
            if (ShopDbAccess.Logic.OrderValidator.IsOrderValid(order))
            {
                context.Orders.Add(order);
            }
        }

        public void DeleteOrderByID(int orderID)
        {
            Order order = GetOrderByID(orderID);
            context.Orders.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            if (ShopDbAccess.Logic.OrderValidator.IsOrderValid(order))
            {
                context.Entry(order).State = EntityState.Modified;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}