using Domain;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UnhappyOrderTests
    {
        [Fact]
        public void Should_throw_orderAlreadyDeliveredException_when_update_order_items()
        {
            var items =new List<OrderItem>() {
                new (1500,"peoduct1",1),
                new (1500,"peoduct2",1),
                new (1500,"peoduct3",1),
            };
            var order = new Order("test2", items);
            order.FinalizeOrder();
            var orderItems = new List<OrderItem>(){
                new (1500,"peoduct-update1",1),
                new (1500,"peoduct-update2",1),
                new (1500,"peoduct-update3",1),
            };
            
            FluentActions.Invoking(() =>
                 order.AddOrderItems(orderItems))
                    .Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void should_throw_OrderAlreadyDeliveredException_when_add_order_items()
        {
            var items = new List<OrderItem>() {
                new (1500,"peoduct1",1),
                new (1500,"peoduct2",1),
                new (1500,"peoduct3",1),
            };
            var order = new Order("test2", items);
            order.FinalizeOrder();
            order.ShipmentOrder("iran -1");            

            FluentActions.Invoking(() =>
                 order.AddOrderItems(new List<OrderItem>() { new(1500, "peoduct-update1", 2) }))
                    .Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void should_throw_OrderItemsNullException_when_create_order()
{
            FluentActions.Invoking(() =>
                 new Order("test2", new()))
                    .Should().Throw<OrderItemsNullException>();
        }

        [Fact]
        public void should_throw_orderStatusNotFinalizedException_when_order_change_status_to_shipment()
        {
            var items = new List<OrderItem>() {
                new (1500,"peoduct1",1),
                new (1500,"peoduct2",1),
                new (1500,"peoduct3",1),
            };
            var order = new Order("test2", items);

            FluentActions.Invoking(() =>
                 order.ShipmentOrder("test address"))
                    .Should().Throw<OrderStatusNotFinalizedException>();
        }
    }
}