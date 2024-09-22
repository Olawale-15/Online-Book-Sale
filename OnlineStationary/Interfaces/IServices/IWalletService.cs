using OnlineStationary.DTOs.WalletDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface IWalletService
    {
 
        BaseResponse FundCustomerWallet(Guid id, decimal amount);
        BaseResponse FundAuthorWallet(Guid id, decimal amount);
        BaseResponse<WalletResponseModel> GetCustomerWallet(Guid id);
        BaseResponse<WalletResponseModel> GetAuthorWallet(Guid id);
        BaseResponse<ICollection<WalletResponseModel>> GetAllWallets();
        BaseResponse<WalletResponseModel> LoggedInUserWallet();
        BaseResponse<WalletResponseModel> GetWallet(Guid id);
        BaseResponse FundWallet(Guid id, decimal amount);
    }
}
