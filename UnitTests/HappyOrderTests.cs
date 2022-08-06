using Domain;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using UnitTests.Builder;

namespace UnitTests
{
    public class HappyOrderTests:TestBase
    {

        [Fact]
        public void Endorse_Order_Status_Is_Created()
        {
            var orderItemBuilder = new OrderItemBuilder();            
            
            var order = new OrderBuilder()
                .WithUsername("iman")
                .WithOrderItems()
                .Build();

            order.Status.Should().Be(OrderStatus.Created);
        }

        [Fact]
        public void Endorse_Order_Item_Is_Created()
        {
            var orderItemBuilder  = new OrderItemBuilder();
            var items = new List<OrderItem>() { 
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
            };
            var order = new OrderBuilder()
                .WithUsername("iman")
                .WithOrderItems(items).Build();
            order.OrderItems.Should().BeEquivalentTo(items);
        }
        
        [Fact]
        public void Endorse_Update_Order_Items()
        {
            var orderItemBuilder = new OrderItemBuilder();
            
            var items = new List<OrderItem>() {
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
            };
            
            var order= new OrderBuilder()
                .WithUsername("iman")
                .WithOrderItems(items).Build();

            var NewItems = new List<OrderItem>() {
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
                orderItemBuilder.Build(),
            };
            order.AddOrderItems(NewItems);

            order.OrderItems.Should().BeEquivalentTo(NewItems);
        }

    }
}