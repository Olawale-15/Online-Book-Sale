using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.Response
{
    public class BookResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Tittle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int PageNumber { get; set; }
        public Category Category { get; set; } = default!;
        public decimal Price { get; set; }
        public string Image { get; set; } = default!;
        public Guid AuthorId { get; set; }
        public int Quantity { get; set; }
    }
}
