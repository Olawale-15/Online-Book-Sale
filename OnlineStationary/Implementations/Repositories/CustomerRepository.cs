using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Customer CreateCustomer(Customer customer)
        {
                _context.
                Customers
                .Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool DeleteCustomer(Customer customer)
        {
                _context.Customers.
                Remove(customer);
            _context.SaveChanges() ;
                 return true;
        }

        public ICollection<Customer> GetAllCustomers()
        {
            var getAllCustomers = _context.Customers.ToList();
            return getAllCustomers;
        }

        public Customer? GetCustomer(Func<Customer, bool> predicate)
        {
            var getCustomer = _context.Customers.FirstOrDefault(predicate);
            return getCustomer;

        }

        public Customer? GetCustomerById(Guid id)
        {
            var getCustomer = _context.Customers.FirstOrDefault(x => x.Id == id);
            return getCustomer;
            
        }

        public Customer UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
