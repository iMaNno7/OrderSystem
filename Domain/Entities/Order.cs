using Domain.Contract;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Order
    {
        private readonly IMessageService _messageService;

        private Order() {
        }
        public int Id { get; set; }
        public string Username { get; private set; }
        public string? Address { get; private set; }
        public Guid TrackingCode { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(string username, List<OrderItem> orderItem, IMessageService messageService)
        {
            Username = username;
            TrackingCode = Guid.NewGuid();
            Status = OrderStatus.Created;
            AddOrderItems(orderItem);

            _messageService = messageService;
            _messageService.SendMessage(username);
        }

        public void AddOrderItems(List<OrderItem> orderItems)
        {
            OrderItems = new();
            CheckCountOrderItems(orderItems);
            CheckCreateOrderStatus();
            OrderItems.AddRange(orderItems);
        }

        public void FinalizeOrder()
        {
            CheckFinalizeOrderStatus();
            Status = OrderStatus.Finalized;
        }
        public void ShipmentOrder(string address)
        {
            CheckShipmentOrderStatus();
            Status = OrderStatus.Shipped;
            Address = address;
        }


        private void CheckCountOrderItems(List<OrderItem> orderItem)
        {
            if (orderItem.Count == 0)
                throw new OrderItemsNullException();
            else if (orderItem.Any(s => s.Count > 3 || s.Count <= 0))
                throw new OrderItemCountException();
        }

        private void CheckCreateOrderStatus()
        {
            if (Status == OrderStatus.Finalized || Status == OrderStatus.Shipped)
                throw new OrderAlreadyDeliveredException();
        }

        private void CheckShipmentOrderStatus()
        {
            if (Status == OrderStatus.Created)
                throw new OrderStatusNotFinalizedException();
        }

        private void CheckFinalizeOrderStatus()
        {
            if (Status == OrderStatus.Finalized || Status == OrderStatus.Shipped)
                throw new OrderAlreadyDeliveredException();
        }

    }
}