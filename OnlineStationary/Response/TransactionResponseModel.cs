namespace OnlineStationary.Response
{
    public class TransactionResponseModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid WalletId { get; set; }
    }
}
