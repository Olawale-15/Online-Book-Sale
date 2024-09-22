namespace OnlineStationary.Response
{
    public class OrderItemResponseModel
    {
        public string BookName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
