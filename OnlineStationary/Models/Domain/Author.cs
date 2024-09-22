using OnlineStationary.Models.Enum;

namespace OnlineStationary.Models.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
