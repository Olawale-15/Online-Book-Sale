using OnlineStationary.Models.Enum;

namespace OnlineStationary.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string Tittle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int PageNumber { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = default!;
        public decimal Price { get; set; }
        public string Image {  get; set; } = default!;
        public Guid AuthorId { get; set; }
        public Author Author {get; set; } = default!;
        public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
    }
}
