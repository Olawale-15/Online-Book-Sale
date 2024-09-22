namespace OnlineStationary.DTOs.OrderDto
{
    public class OrderItemRequestModel
    {
        public Guid BookId { get; set; } = default!;
        public int Quantity { get; set; } = default!;
    }
}
