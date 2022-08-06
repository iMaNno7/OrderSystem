using Domain.Entities;
using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builder
{
    public class OrderItemBuilder
    {
        private int _price;
        private string _title;
        private int _count;
        private int _id;

        public OrderItemBuilder()
        {
            generateRandomId();
            _count = 2;
            _title = "defult builder title";
            _price = 800;
        }

        public OrderItemBuilder WithCount(int count)
        {
            _count = count;
            return this;
        }

        public OrderItemBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public OrderItemBuilder WithPrice(int price)
        {
            _price = price;
            return this;
        }

        public OrderItem Build() => new OrderItem(_price, _title, _count);

        private void generateRandomId()
        {
            var id = new Random().Next();
            if (_id == id)
                generateRandomId();
            _id = id;
        }
    }
}
