using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.DTOs.CustomerDto
{
    public class CustomerUpdateRequestModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
