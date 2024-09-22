using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        Order GetOrderById(Guid id);
        ICollection<Order> GetAllOrders();
        void Save();
    }
}
