namespace OnlineStationary.Models.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = default!;
    }
}
