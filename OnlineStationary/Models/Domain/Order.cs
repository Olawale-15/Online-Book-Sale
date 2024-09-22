using OnlineStationary.Models.Enum;

namespace OnlineStationary.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();

    }
}
