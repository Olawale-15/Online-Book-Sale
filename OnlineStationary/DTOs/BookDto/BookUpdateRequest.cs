using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.DTOs.BookDto
{
    public class BookUpdateRequest
    {
        public string Name { get; set; } = default!;
        public string Tittle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int PageNumber { get; set; }
        public Category Category { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; } = default!;
        public Guid AuthorId { get; set; }
    }
}
