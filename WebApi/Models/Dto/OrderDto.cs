using Domain.Entities;

namespace WebApi.Models.Dto
{
    public class OrderDto
    {
        public string Username { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
