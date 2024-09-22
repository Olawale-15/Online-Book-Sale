using OnlineStationary.Models.Domain;
using OnlineStationary.Models.Enum;

namespace OnlineStationary.Response
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public ICollection<OrderItemResponseModel> OrderItemResponseModels { get; set; } = new List<OrderItemResponseModel>();
    }
}
