using OnlineStationary.DTOs.CustomerDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Response;

namespace OnlineStationary.Implementations.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWalletRepository _walletRepository;
        public CustomerService(IUserRepository userRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository, IWalletRepository walletRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _walletRepository = walletRepository;
        }
        public BaseResponse CreateCustomer(CustomerRequestModel customerRequest)
        {
            var getEmail = _userRepository.GetUser(x => x.Email == customerRequest.Email);
            if (getEmail != null)
            {
                return new BaseResponse
                {
                    Message = "Email already exist",
                    IsSucessful = false,
                };
            }
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(customerRequest.Password, salt);

            var user = new User
            {
                Email = customerRequest.Email,

                Username = $"{customerRequest.FirstName}{customerRequest.LastName}",

                Salt = salt,

                PasswordHash = hashPassword,
            };
            _userRepository.CreateUser(user);
            _userRepository.Save();

            var customer = new Customer
            {
                FirstName = customerRequest.FirstName,

                LastName = customerRequest.LastName,

                Email = customerRequest.Email,

                PhoneNumber = customerRequest.PhoneNumber,

                Gender = customerRequest.Gender,

                Address = customerRequest.Address,
            };
            _customerRepository.CreateCustomer(customer);
            _customerRepository.Save();

            var role = new Role
            {
                Name = "Customer"
            };
            _roleRepository.CreateRole(role);
            _roleRepository.Save();

            var userRole = new UserRole
            {
                UserId = user.Id,

                User = user,

                RoleId = role.Id,

                Role = role,
            };
            user.UserRoles.Add(userRole);

            var wallet = new Wallet
            {
                Balance = 0m,
                UserId = user.Id,
                User = user,
            };
            _walletRepository.CreateWallet(wallet);
            _walletRepository.Save();
 
            return new BaseResponse
            {
                Message = "Registration sucessful",
                IsSucessful = true,
            };
        }

        public BaseResponse DeleteCustomer(Guid id)
        {
            var getCustomer = _customerRepository.GetCustomerById(id);
            if (getCustomer == null) {
                return new BaseResponse
                {
                    Message = "Customer not found",
                    IsSucessful = false,
                };
            }

            _customerRepository.DeleteCustomer(getCustomer);
            _customerRepository.Save();

            return new BaseResponse
            {
                Message = "Customer deleted successful",
                IsSucessful = true,
            };
        }

        public BaseResponse<ICollection<CustomerResponseModel>> GetAllCustomers()
        {
            var getCustomer = _customerRepository.GetAllCustomers();
            if (getCustomer == null) {
                return new BaseResponse<ICollection<CustomerResponseModel>>
                {
                    Message = "Customer list not found",
                    IsSucessful = false,
                };
            }

            var cutomers = getCustomer.Select(x => new CustomerResponseModel()
            {
                Id = x.Id,

                Username = $"{x.FirstName}{x.LastName}",

                Email = x.Email,

                Address = x.Address,

                PhoneNumber = x.PhoneNumber,

                Gender = x.Gender,
            }).ToList();

            return new BaseResponse<ICollection<CustomerResponseModel>>
            {
                Data = cutomers,
                Message = "List of customers",
                IsSucessful = true
            };
        }

        public BaseResponse<CustomerResponseModel> GetCustomer(Guid id)
        {
            var getCustomer = _customerRepository.GetCustomerById(id);
            if (getCustomer == null) {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "customer not found",
                    IsSucessful = false,
                };
            }

            var customer = new CustomerResponseModel
            {
                Id = getCustomer.Id,

                Username = $"{getCustomer.FirstName}{getCustomer.LastName}",

                Email = getCustomer.Email,

                Address = getCustomer.Address,

                PhoneNumber = getCustomer.PhoneNumber,

                Gender = getCustomer.Gender,

            };
            return new BaseResponse<CustomerResponseModel>
            {
                Data = customer,

                Message = "Customer details",

                IsSucessful = true
            };
        }

        public BaseResponse UpdateCustomer(Guid id, CustomerUpdateRequestModel customerUpdateRequest)
        {
            var getCustomer = _customerRepository.GetCustomerById(id);
            if (getCustomer == null) {
                return new BaseResponse
                {
                    Message = "Customer not found",
                    IsSucessful = false,
                };
            }

            var customer = new Customer();

            customer.FirstName = customerUpdateRequest.FirstName;

            customer.LastName = customerUpdateRequest.LastName;

            customer.Email = customerUpdateRequest.Email;

            customer.Address = customerUpdateRequest.Address;

            customer.PhoneNumber = customerUpdateRequest.PhoneNumber;
           
            _customerRepository.UpdateCustomer(customer);
            _customerRepository.Save();
            return new BaseResponse
            {
                Message = "Customer updated sucessfully",
                IsSucessful = true,
            };

        }
    }
}
