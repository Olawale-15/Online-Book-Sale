

using OnlineStationary.DTOs.UserDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Response;
using System.Linq;

namespace OnlineStationary.Implementations.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public BaseResponse<UserResponseModel> Login(LoginRequestModel loginRequest)
        {
            var getUser = _userRepository.GetUser( u => u.Email == loginRequest.Email);
            if (getUser == null) {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "Email not registered",
                    IsSucessful = true,
                };
            }

            var password = BCrypt.Net.BCrypt.Verify(loginRequest.Password, getUser.PasswordHash);
            if (!password)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "Invalid credntial",
                    IsSucessful = false,
                };
            }

            return new BaseResponse<UserResponseModel>
            {
                Message = "Login successful",
                IsSucessful = true,
                Data = new UserResponseModel
                {
                    Id = getUser.Id,
                    Username = getUser.Username,
                    Email = getUser.Email,
                    UserRoles = getUser.UserRoles.Select(x => new Role
                    {
                        Id = x.Role.Id, 
                        Name = x.Role.Name
                    }).ToList(),
                 }              
            };
        }

    }
}
