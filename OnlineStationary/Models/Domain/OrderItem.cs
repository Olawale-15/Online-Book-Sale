namespace OnlineStationary.Models.Domain
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;
    }
}
