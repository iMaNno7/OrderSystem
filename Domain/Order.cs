namespace Domain
{
    public class Order
    {
        public string Username { get; set; }
        public string Address { get; set; }
        public string TrackingCode { get; set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(string username, string trackingCode)
        {
            Username = username;
            TrackingCode = trackingCode;
            Status = OrderStatus.Created;
        }

        public void InsertOrderItem(List<OrderItem> orderItems)
        {
            OrderItems=new();
            CheckOrderItems(orderItems);
            CheckCreateOrderStatus();
            OrderItems.AddRange(orderItems);
        }

        public void AddItem(OrderItem orderItem)
        {
            CheckCreateOrderStatus();
            OrderItems.Add(orderItem);
        }
        public void Finalize()
        {
            CheckFinalizeOrderStatus();
            Status=OrderStatus.Finalized;
        }
        public void Shipment(string address)
        {
            CheckShipmentOrderStatus();
            Status=OrderStatus.Shipped;
            Address=address;
        }


        private void CheckOrderItems(List<OrderItem> orderItem)
        {
            if (orderItem.Count > 3 || orderItem.Count <= 0)
                throw new OrderItemsNullException();
        }

        private void CheckCreateOrderStatus()
        {
            if (this.Status == OrderStatus.Finalized || this.Status == OrderStatus.Shipped)
                throw new OrderAlreadyDeliveredException();
        }

        private void CheckShipmentOrderStatus()
        {
            if (this.Status == OrderStatus.Created)
                throw new OrderStatusNotFinalizedException();
        }

        private void CheckFinalizeOrderStatus()
        {
            if (this.Status == OrderStatus.Finalized || this.Status == OrderStatus.Shipped)
                throw new OrderAlreadyDeliveredException();
        }

    }

    public enum OrderStatus
    {
        Created,
        Finalized,
        Shipped
    }
}