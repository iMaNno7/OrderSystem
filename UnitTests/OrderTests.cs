using Domain;
using FluentAssertions;

namespace UnitTests
{
    public class OrderTests
    {
        [Fact]
        public void should_insert_order_items()
        {
            var order = new Order("test2", "tr-1234546");
            var items = new List<OrderItem>() {
                new (1500,"peoduct1"),
                new (1500,"peoduct1"),
                new (1500,"peoduct1"),
            };
            order.InsertOrderItem(items);
            order.OrderItems.Should().BeEquivalentTo(items);
        }
        
        [Fact]
        public void should_update_order_items()
        {
            var order = new Order("test2", "tr-1234546");
            order.InsertOrderItem(new() {
                new (1500,"peoduct1"),
                new (1500,"peoduct2"),
                new (1500,"peoduct3"),
            });

            var orderItems = new List<OrderItem>(){
                new (1500,"peoduct-update1"),
                new (1500,"peoduct-update2"),
                new (1500,"peoduct-update3"),
            };
            order.InsertOrderItem(orderItems);

            order.OrderItems.Should().Contain(orderItems);
        }

        [Fact]
        public void Should_throw_orderAlreadyDeliveredException_when_update_order_items()
        {
            var order = new Order("test2", "tr-1234546");
            order.InsertOrderItem(new() {
                new (1500,"peoduct1"),
                new (1500,"peoduct2"),
                new (1500,"peoduct3"),
            });
            order.Finalize();
            var orderItems = new List<OrderItem>(){
                new (1500,"peoduct-update1"),
                new (1500,"peoduct-update2"),
                new (1500,"peoduct-update3"),
            };
            
            FluentActions.Invoking(() =>
                 order.InsertOrderItem(orderItems))
                    .Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void should_throw_OrderAlreadyDeliveredException_when_add_order_items()
        {
            var order = new Order("test2", "tr-1234546");
            order.InsertOrderItem(new() {
                new (1500,"peoduct1"),
                new (1500,"peoduct2"),
                new (1500,"peoduct3"),
            });
            order.Finalize();
            order.Shipment("iran -1");            

            FluentActions.Invoking(() =>
                 order.AddItem(new(1500, "peoduct-update1")))
                    .Should().Throw<OrderAlreadyDeliveredException>();
        }

        [Fact]
        public void should_throw_OrderItemsNullException_when_create_order()
        {
            var order = new Order("test2", "tr-1234546");            
            FluentActions.Invoking(() =>
                 order.InsertOrderItem(new()))
                    .Should().Throw<OrderItemsNullException>();
        }

        [Fact]
        public void should_throw_orderStatusNotFinalizedException_when_order_change_status_to_shipment()
        {
            var order = new Order("test2", "tr-1234546");
            
            order.InsertOrderItem(new() {
                new (1500,"peoduct1"),
                new (1500,"peoduct2"),
                new (1500,"peoduct3"),
            });

            FluentActions.Invoking(() =>
                 order.Shipment("test address"))
                    .Should().Throw<OrderStatusNotFinalizedException>();
        }
    }
}