namespace OnlineStationary.Models.Domain
{
    public class OrderBook
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
