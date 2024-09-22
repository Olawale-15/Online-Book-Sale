using OnlineStationary.Models.Domain;

namespace OnlineStationary.Response
{
    public class WalletResponseModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
