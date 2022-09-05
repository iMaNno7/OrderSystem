using Domain.Contract;
using Domain.Entities;
using GenFu;
using NSubstitute;
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
        private IMessageService _messageService;
        private List<OrderItem> _orderItems;

        public OrderBuilder()
        {
            _orderItems = new();
            _messageService = Substitute.For<IMessageService>();
        }

        public OrderBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public OrderBuilder WithMessageService(IMessageService messageService)
        {
            _messageService = messageService;
            return this;
        }

        public OrderBuilder WithOrderItems(List<OrderItem> orderItems= null)
        {
            _orderItems = orderItems ?? new() {
                new(1000,"",2),
                new(500,"",2),
                new(800,"",2),
            };
            return this;
        }

        public OrderBuilder WithOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
            return this;
        }

        public Order Build() => new Order(_username, _orderItems,_messageService);
    }
}
