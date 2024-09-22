using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface ITransactionService
    {
        BaseResponse<ICollection<TransactionResponseModel>> GetAllTransaction();
    }
}
