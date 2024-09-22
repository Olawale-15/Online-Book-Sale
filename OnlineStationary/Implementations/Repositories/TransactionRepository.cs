using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _context;
        public TransactionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Transaction Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public bool Delete(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public Transaction? GetTransaction(Func<Transaction, bool> predicate)
        {
            var getTransaction = _context.Transactions.FirstOrDefault(predicate);
            return getTransaction;
        }

        public Transaction? GetTransactionById(Guid id)
        {
            var getTransaction = _context.Transactions.
                Include(x => x.Wallet).FirstOrDefault
                (x => x.Id == id);
            return getTransaction;
        }

        public Transaction Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges ();
            return transaction;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
