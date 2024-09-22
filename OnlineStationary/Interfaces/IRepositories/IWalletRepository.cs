using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IWalletRepository
    {
        Wallet CreateWallet(Wallet wallet);
        Wallet UpdateWallet(Wallet wallet);
        bool DeleteWallet(Wallet wallet);
        Wallet? GetWalletById(Guid id);
        Wallet? GetWallet(Func<Wallet, bool> predicate);
        Wallet? GetCustomerWallet(Guid id);
        Wallet? GetAuthorWallet(Guid id);
        ICollection<Wallet> GetAllWallets();

        void Save();
    }
}
