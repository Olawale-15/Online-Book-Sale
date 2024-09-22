using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Response;

namespace OnlineStationary.Implementations.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public BaseResponse<ICollection<TransactionResponseModel>> GetAllTransaction()
        {
            var getAllTransactions = _transactionRepository.GetAllTransactions();
            if (getAllTransactions == null) {
                return new BaseResponse<ICollection<TransactionResponseModel>> { 
                    Message = "Transactions not available",
                    IsSucessful = false,
                    Data = null,
                };
            }

            var transactions = getAllTransactions.Select(x => new TransactionResponseModel()
            {
                Id = x.Id,
                Amount = x.Amount,
                TransactionDate = x.TransactionDate,
                WalletId = x.WalletId,
            }).ToList();

            return new BaseResponse<ICollection<TransactionResponseModel>>
            {
                Data = transactions,
                Message = "List of transactions",
                IsSucessful = true
            };
        }
    }
}
