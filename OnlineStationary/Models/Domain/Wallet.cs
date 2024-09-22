namespace OnlineStationary.Models.Domain
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
