using Domain.Contract;
using Domain.Entities;
using GenFu;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Factories
{
    public class OrderFactory
    {
        private readonly string _username = "test";
        private readonly List<OrderItem> _orderItems = new() {
        new(100,"product",2)
        };
        public Order GetSomeOrder()
        {
            return new(_username, _orderItems, Substitute.For<IMessageService>());
        }
    }
}
