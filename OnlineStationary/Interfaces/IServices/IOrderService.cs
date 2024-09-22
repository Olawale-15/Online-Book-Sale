using OnlineStationary.DTOs.OrderDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface IOrderService
    {
        BaseResponse CreateOrder(OrderRequestModel orderRequest);
        BaseResponse DeleteOrder(Guid id);
        BaseResponse<OrderResponseModel> GetOrder(Guid id);
        BaseResponse<ICollection<OrderResponseModel>> GetOrders();
    }
}
