namespace Domain.Entities
{
    public class OrderItem
    {
        private OrderItem() { }
        public OrderItem(int price, string title, int count)
        {
            Price = price;
            Title = title;
            Count = count;
        }
        public int Id { get; set; }
        public int Price { get; }
        public string Title { get; }
        public int Count { get; }
    }

}