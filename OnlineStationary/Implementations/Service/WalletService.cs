using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using OnlineStationary.DTOs.WalletDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Response;
using System.Security.Claims;

namespace OnlineStationary.Implementations.Service
{
    public class WalletService : IWalletService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public WalletService(IAuthorRepository authorRepository, ICustomerRepository customerRepository, ITransactionRepository transactionRepository, IWalletRepository walletRepository, ICurrentUserRepository currentUserRepository, IUserRepository userRepository, IHttpContextAccessor contextAccessor)

        {
            _authorRepository = authorRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _currentUserRepository = currentUserRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public BaseResponse<WalletResponseModel> LoggedInUserWallet()
        {
            var getLoggedInUser = _contextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var wallet = _walletRepository.GetWallet(x => x.UserId.ToString() == getLoggedInUser);
            if (wallet == null) {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "User wallet not found",
                    IsSucessful = false
                };
            }

            return new BaseResponse<WalletResponseModel>
            {
                Message = "Wallet found sucessfully",
                IsSucessful = true,
                Data = new WalletResponseModel
                {
                    Balance = wallet.Balance,
                    Id = wallet.Id,
                    UserId = wallet.UserId
                }
            };
        }

        public BaseResponse<WalletResponseModel> GetWallet(Guid id)
        {
            var wallet = _walletRepository.GetWalletById(id);
            if(wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "Wallet not found",
                    IsSucessful = false
                };
            }

            var walletInfo = new WalletResponseModel
            {
                Balance = wallet.Balance,
                Id = wallet.Id,
                UserId = wallet.UserId
            };

            return new BaseResponse<WalletResponseModel>
            {
                Message = "Wallet found sucessfully",
                IsSucessful = true,
                Data = walletInfo,
            };
        }

        public BaseResponse FundWallet(Guid id, decimal amount)
        {
            var getWalletTofund = _walletRepository.GetWalletById(id);
            if(getWalletTofund == null)
            {
                return new BaseResponse
                {
                    Message = "wallet not found",
                    IsSucessful = false
                };
            }

            if(amount < 100)
            {
                return new BaseResponse
                {
                    Message = "you can not fund wallet below 100",
                    IsSucessful = false
                };
            }

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                WalletId = getWalletTofund.Id,
                Wallet = getWalletTofund
            };
            _transactionRepository.Create(transaction);
            _transactionRepository.Save();

            getWalletTofund.Balance += amount;
            _walletRepository.UpdateWallet(getWalletTofund);
            _walletRepository.Save();

            return new BaseResponse
            {
                Message = "Wallet credited sucessfully",
                IsSucessful = true,
            };
        }



        public BaseResponse FundAuthorWallet(Guid id, decimal amount)
        {
            var getAuthorWallet = _walletRepository.GetAuthorWallet(id);
            if (getAuthorWallet == null) {
                return new BaseResponse
                {
                    Message = "Author wallet not found",
                    IsSucessful = false,
                };
            }

            if(amount < 100)
            {
                return new BaseResponse
                {
                    Message = "you can not fund wallet without amount below 100",
                    IsSucessful = false,
                };
            }

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Wallet = getAuthorWallet
            };

            _transactionRepository.Create(transaction);
            _transactionRepository.Save();

            getAuthorWallet.Balance += amount;
            _walletRepository.UpdateWallet(getAuthorWallet);
            _walletRepository.Save();
            return new BaseResponse
            {
                Message = "Wallet credited sucessfully",
                IsSucessful = true,
            };

        }


        public BaseResponse FundCustomerWallet(Guid id, decimal amount)
        {
            var getCutomerWallet = _walletRepository.GetCustomerWallet(id);
            if (getCutomerWallet == null) {
                return new BaseResponse
                {
                    Message = "Customer wallet not found",
                    IsSucessful = false,
                };
            }

            if (amount < 100) {
                return new BaseResponse
                {
                    Message = "You can not fund wallet amount below 100",
                    IsSucessful = false,
                };
            }

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Wallet = getCutomerWallet
            };
            _transactionRepository.Create(transaction);
            _transactionRepository.Save();

            getCutomerWallet.Balance += amount;
            _walletRepository.UpdateWallet(getCutomerWallet);
            _walletRepository.Save();

            return new BaseResponse
            {
                Message = "Wallet credited sucessfully",
                IsSucessful = true,
            };
        }

        public BaseResponse<ICollection<WalletResponseModel>> GetAllWallets()
        {
            var getWallets = _walletRepository.GetAllWallets();
            if (getWallets == null) {
                return new BaseResponse<ICollection<WalletResponseModel>>
                {
                    Message = "No wallet found",
                    IsSucessful = false,
                    Data = null,
                };
            }

            var wallets = getWallets.Select(x => new WalletResponseModel()
            {
                Id = x.Id,
                Balance = x.Balance,
                Transactions = x.Transactions.Select(x => new Transaction()
                {
                    Amount = x.Amount,
                    TransactionDate = x.TransactionDate,
                    Wallet = x.Wallet,
                    WalletId = x.WalletId,
                }).ToList()

            }).ToList();

            return new BaseResponse<ICollection<WalletResponseModel>>
            {
                Data = wallets,
                Message = "List of wallets",
                IsSucessful = true,
            };
        }

        public BaseResponse<WalletResponseModel> GetAuthorWallet(Guid id)
        {
            var getAuthorWallet = _walletRepository.GetAuthorWallet(id);
            if (getAuthorWallet == null) {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "Author id not found",
                    IsSucessful = false,
                };
            }

            var authorWallet = new WalletResponseModel
            {
                Id = getAuthorWallet.Id,
                Transactions = getAuthorWallet.Transactions.Select(x => new Transaction()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    TransactionDate = x.TransactionDate,
                    WalletId = x.WalletId,
                    Wallet = getAuthorWallet
                }).ToList()
            };

            return new BaseResponse<WalletResponseModel>
            {
                Data = authorWallet,
                Message = "Author wallet inmformation",
                IsSucessful = true,
            };
        }

        public BaseResponse<WalletResponseModel> GetCustomerWallet(Guid id)
        {
            var getCustomerWallet = _walletRepository.GetCustomerWallet(id);
            if (getCustomerWallet == null) {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "Customer wallet not found",
                    IsSucessful = false,
                    Data = null
                };
            }

            var customerWallet = new WalletResponseModel
            {
                Id = getCustomerWallet.Id,
             
                Balance = getCustomerWallet.Balance,
                Transactions = getCustomerWallet.Transactions.Select(x => new Transaction()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    TransactionDate = x.TransactionDate,
                    WalletId = x.WalletId,
                    Wallet = getCustomerWallet
                }).ToList()
            };
            return new BaseResponse<WalletResponseModel>
            {
                Data = customerWallet,
                Message = "Customer wallet details",
                IsSucessful = true,
            };
        }
    }
}
