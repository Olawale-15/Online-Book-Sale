using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        Customer? GetCustomerById(Guid id);
        Customer? GetCustomer(Func<Customer, bool> predicate);
        ICollection<Customer> GetAllCustomers();
        void Save();
    }
}
