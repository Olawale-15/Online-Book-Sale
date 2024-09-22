using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface ITransactionRepository
    {
        Transaction Create(Transaction transaction);
        Transaction Update(Transaction transaction);
        bool Delete(Transaction transaction);
        Transaction? GetTransaction(Func<Transaction, bool> predicate);
        Transaction? GetTransactionById(Guid id);
        ICollection<Transaction> GetAllTransactions();
        void Save();
    }
}
