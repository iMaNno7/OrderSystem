namespace Domain.Entities
{
    public record class OrderItem(int Price, string Title, int Count)
    {
        public int Id { get; set; }
    }

}