using Domain.Entities;
using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builder
{
    public  class OrderBuilder
    {
        private string _username;
        private List<OrderItem> _orderItems;

        public OrderBuilder()
        {
            _orderItems = new();
        }

        public OrderBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public OrderBuilder WithOrderItems(List<OrderItem> orderItems= null)
        {
            _orderItems = orderItems ?? new() {
                A.New<OrderItem>(new(1000,"",2)),
                A.New<OrderItem>(new(500,"",2)),
                A.New<OrderItem>(new(800,"",2)),
            };
            return this;
        }

        public OrderBuilder WithOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
            return this;
        }

        public Order Build() => new Order(_username, _orderItems);
    }
}
