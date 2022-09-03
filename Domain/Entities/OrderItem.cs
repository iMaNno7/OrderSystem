namespace Domain.Entities
{
    public class OrderItem
    {
        private OrderItem() { }
        
        public OrderItem(int price, string title, int count)
        {
            this.Price = price;
            Title = title;
            Count = count;
        }

        public int Id { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }

}