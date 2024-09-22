using OnlineStationary.Models.Domain;

namespace OnlineStationary.DTOs.WalletDto
{
    public class WalletRequestModel
    {
        public Guid AuthorId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
