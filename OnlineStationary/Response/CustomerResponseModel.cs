using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.Response
{
    public class CustomerResponseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
    }
}
