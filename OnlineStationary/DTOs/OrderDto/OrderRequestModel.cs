using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.DTOs.OrderDto
{
    public class OrderRequestModel
    {
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AuthorId { get; set; }  
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public Book Book { get; set; } = default!;
    }
}

