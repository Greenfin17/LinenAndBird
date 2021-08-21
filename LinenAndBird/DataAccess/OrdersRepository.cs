using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;

namespace LinenAndBird.DataAccess
{
    public class OrdersRepository
    {
        public Guid Id { get; set; }

        static List<Order> _orders = new List<Order>();


        internal void Add(Order order)
        {
            order.Id = Guid.NewGuid();
            _orders.Add(order);
        }
    }
}
