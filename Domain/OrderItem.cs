namespace Domain
{
    public class OrderItem
    {
        public OrderItem(int price, string title = null)
        {
            Price = price;
            Title = title;
        }

        public string Title { get; set; }
        public int Price { get; set; }
    }
}