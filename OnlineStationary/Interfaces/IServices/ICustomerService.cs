using OnlineStationary.DTOs.CustomerDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface ICustomerService
    {
        BaseResponse CreateCustomer(CustomerRequestModel customerRequest);
        BaseResponse UpdateCustomer(Guid id, CustomerUpdateRequestModel customerUpdateRequest);
        BaseResponse DeleteCustomer(Guid id);
        BaseResponse<CustomerResponseModel> GetCustomer(Guid id);
        BaseResponse<ICollection<CustomerResponseModel>> GetAllCustomers();
        
    }
}
