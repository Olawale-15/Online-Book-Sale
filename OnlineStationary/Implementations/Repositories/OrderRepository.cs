using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public bool DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Order> GetAllOrders()
        {
            var getAllOrder = _context.Orders.ToList();
            return getAllOrder;
        }

        public Order GetOrderById(Guid id)
        {
            var getOrder = _context.Orders.FirstOrDefault(o => o.Id == id);
            return getOrder;
        }

        public Order UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
