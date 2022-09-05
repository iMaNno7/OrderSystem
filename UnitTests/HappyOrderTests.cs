using Domain;
using Domain.Contract;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using NSubstitute;
using UnitTests.Builder;
using UnitTests.Factories;

namespace UnitTests
{
    public class HappyOrderTests : TestBase
    {
        public HappyOrderTests()
        {

        }
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
            var orderItemBuilder = new OrderItemBuilder();
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
        public void Should_Find_Order_By_OrderId()
        {
            var orderFactory = new OrderFactory();
            var mock = new Mock<IOrderRepository>();
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(orderFactory.GetSomeOrder());
            IOrderRepository orderRepository = mock.Object;

            var order= orderRepository.GetById(5);

            order.Should().NotBeNull();
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

            var order = new OrderBuilder()
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

        [Theory]
        [InlineData("ali")]
        [InlineData("iman")]
        [InlineData("sara")]
        public void Endorse_MessageService_Send_Message_With_Username(string userName)
        {
            var orderItemBuilder = new OrderItemBuilder();
            var messageService = Substitute.For<IMessageService>(); 
            var order = new OrderBuilder()
                .WithUsername(userName)
                .WithOrderItems()
                .WithMessageService(messageService)
                .Build();

            order.Status.Should().Be(OrderStatus.Created);
            messageService.Received().SendMessage(userName);
        }
    }
}