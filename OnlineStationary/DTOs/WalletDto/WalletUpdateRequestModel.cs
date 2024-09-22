namespace OnlineStationary.DTOs.WalletDto
{
    public class WalletUpdateRequestModel
    { 
        public Guid CustomerId{ get; set; }
        public Guid AuthorId{ get; set; }
        public decimal Balance { get; set; }
    }
}
