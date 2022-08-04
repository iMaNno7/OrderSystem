using Domain;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;

namespace UnitTests
{
    public class HappyOrderTests:TestBase
    {
        [Fact]
        public void should_insert_order_items()
        {
            var items = new List<OrderItem>() {
                new (1500,"peoduct1",2),
                new (1500,"peoduct1",1),
                new (1500,"peoduct1",3),
            };
            var order = new Order("test2", items);
            order.OrderItems.Should().BeEquivalentTo(items);
        }
        
        [Fact]
        public void should_update_order_items()
        {
            var items=new List<OrderItem>(){
                new (1500,"peoduct1",1),
                new (1500,"peoduct2",2),
                new (1500,"peoduct3",3),
            };
            var order = new Order("test2",items);

            var orderItems = new List<OrderItem>(){
                new (1500,"peoduct-update1",1),
                new (1500,"peoduct-update2",2),
                new (1500,"peoduct-update3",3),
            };
            order.AddOrderItems(orderItems);

            order.OrderItems.Should().BeEquivalentTo(orderItems);
        }

    }
}