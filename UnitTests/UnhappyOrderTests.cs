using Domain;
using Domain.Contract;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using UnitTests.Builder;
using UnitTests.Factories;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTests
{
    public class UnhappyOrderTests
    {
        [Fact]
        public void Should_throw_orderAlreadyDeliveredException_when_update_order_items()
        {
            var items = new List<OrderItem>() {
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
            };
            var orderItems = new List<OrderItem>(){
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),            };

            var order = new OrderBuilder().WithUsername("IMAN").WithOrderItems(items).Build();
            order.FinalizeOrder();
            var addOrderItem = () =>
                 order.AddOrderItems(orderItems);

            addOrderItem.Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void Should_Throw_OrderAlreadyDeliveredException_When_Add_Order_Items()
        {
            var items = new List<OrderItem>() {
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
            };
            var order = new OrderBuilder().WithUsername("iman").WithOrderItems(items).Build();

            order.FinalizeOrder();
            order.ShipmentOrder("iran -1");

            var addOrderItems = () =>
                 order.AddOrderItems(items);

            addOrderItems.Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void Should_Throw_OrderItemsNullException_When_Create_Order()
        {
            var order = () => new OrderBuilder().WithUsername("iman")
                .WithOrderItems(new()).Build();

            order.Should().Throw<OrderItemsNullException>();
        }
        
        [Fact]
        public void Should_Find_Order_By_OrderId()
        {
            var mock = new Mock<IOrderRepository>();
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(()=> {
                throw new OrderNotFoundException();
            });
            IOrderRepository orderRepository = mock.Object;
            
            var getById=()=> orderRepository.GetById(5);

            getById.Should().Throw<OrderNotFoundException>();
        }

        [Fact]
        public void Should_Throw_OrderStatusNotFinalizedException_When_Order_Change_Status_To_Shipment()
        {
            var items = new List<OrderItem>() {
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
                new OrderItemBuilder().Build(),
            };

            var order = new OrderBuilder()
                .WithOrderItems(items)
                .WithUsername("test2").Build();

            var shipmentOrder = () =>
                 order.ShipmentOrder("test address");

            shipmentOrder
                    .Should().Throw<OrderStatusNotFinalizedException>();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(0)]
        [InlineData(-2)]
        public void Should_Throw_OrderItemCountException_When_Create_Order(int cout)
        {
            var items = new List<OrderItem>() {
                new OrderItemBuilder().WithCount(cout).Build(),
            };
            var createOrder =()=> new OrderBuilder()
                .WithOrderItems(items)
                .WithUsername("test2").Build();

            createOrder
                    .Should().Throw<OrderItemCountException>();
        }
    }
}