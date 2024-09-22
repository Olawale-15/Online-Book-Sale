using Microsoft.AspNetCore.Identity.Data;
using OnlineStationary.DTOs.UserDto;
using OnlineStationary.Response;

namespace OnlineStationary.Interfaces.IServices
{
    public interface IUserService
    {
        BaseResponse<UserResponseModel> Login(LoginRequestModel LoginRequestModel);
    }
}
