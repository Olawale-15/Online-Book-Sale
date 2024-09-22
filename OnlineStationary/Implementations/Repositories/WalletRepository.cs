using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;
using ZstdSharp.Unsafe;

namespace OnlineStationary.Implementations.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationContext _context;
        public WalletRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Wallet CreateWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);

            _context.SaveChanges();
            return wallet;
        }

        public bool DeleteWallet(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Wallet> GetAllWallets()
        {
            var getWallets = _context.Wallets.ToList();
            return getWallets;
        }

        public Wallet? GetWallet(Func<Wallet, bool> predicate)
        {
            var getWallet = _context.Wallets.FirstOrDefault(predicate);
            return getWallet;
        }

        public Wallet? GetWalletById(Guid id)
        {
            var getWallet = _context.Wallets
                .FirstOrDefault(x => x.Id == id);
            return getWallet;
        }

        public Wallet UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
            return wallet;
        }

        public Wallet? GetCustomerWallet(Guid id)
        {
            var getCustomerWallet = _context.Wallets.FirstOrDefault();
            return getCustomerWallet;
        }

        public Wallet? GetAuthorWallet(Guid id)
        {
            var getAuthorWallet = _context.Wallets.FirstOrDefault(x => x.Id == id);
            return getAuthorWallet;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

      
    }
}
