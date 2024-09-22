using OnlineStationary.DTOs.AuthorDto;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Interfaces.IServices;
using OnlineStationary.Models.Domain;
using OnlineStationary.Response;

namespace OnlineStationary.Implementations.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthorService(IUserRepository userRepository, IAuthorRepository authorRepository, IWalletRepository walletRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _authorRepository = authorRepository;
            _walletRepository = walletRepository;
            _roleRepository = roleRepository;
        }

        public BaseResponse CreateAuthor(AuthorRequestModel authorRequest)
        {
            var getUser = _userRepository.GetUser(x => x.Email ==  authorRequest.Email);
            if (getUser != null) 
            {
                return new BaseResponse
                {
                    Message = $"{getUser.Email} already exist",
                    IsSucessful = false ,
                };
            }
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(authorRequest.Password, salt);

            var user = new User
            {
                Username = $"{authorRequest.FirstName}{authorRequest.LastName}",
                Email = authorRequest.Email,
                Salt = salt,
                PasswordHash = hashPassword
            };
            _userRepository.CreateUser(user);
            _userRepository.Save();

            var author = new Author
            {
                FirstName = authorRequest.FirstName,

                LastName = authorRequest.LastName,

                Email = authorRequest.Email,

                Gender = authorRequest.Gender,

                Address = authorRequest.Address,

                PhoneNumber = authorRequest.PhoneNumber,
            };
            _authorRepository.CreateAuthor(author);
           

            var role = _roleRepository.GetRole(x => x.Name == "Author");
            var userRole = new UserRole
            {
                UserId = user.Id,

                User = user,

                RoleId = role.Id,

                Role = role,
            };

            user.UserRoles.Add(userRole);
            _roleRepository.Save();

            var wallet = new Wallet
            {
                Balance = 0m,
                UserId = user.Id,
                User = user,
            };
            _walletRepository.CreateWallet(wallet);
            _walletRepository.Save();

            _userRepository.Save();
            return new BaseResponse
            {
                Message = "Registration sucessful",
                IsSucessful = true,
            };
        }

        public BaseResponse DeleteAuthor(Guid id)
        {
            var getAuthor = _authorRepository.GetAuthorById(id);
            if (getAuthor == null)
            {
                return new BaseResponse
                {
                    Message = "Author not found",
                    IsSucessful = false,
                };
            }
            _authorRepository.DeleteAuthor(getAuthor);
            return new BaseResponse
            {
                Message = "Author deleted",
                IsSucessful = true,
            };
        }

        public BaseResponse<ICollection<AuthorResponseModel>> GetAllAuthors()
        {
            var getAuthors = _authorRepository.GetAllAuthors();
            if (getAuthors == null)
            {
                return new BaseResponse<ICollection<AuthorResponseModel>>
                {
                    Message = "Authors not found",
                    IsSucessful = false,
                };
            }

            var authors = getAuthors.Select(x => new AuthorResponseModel()
            {
                Id = x.Id,

                Username = $"{x.FirstName}{x.LastName}",

                Email = x.Email,

                Gender = x.Gender,

                Address = x.Address,

                PhoneNumber = x.PhoneNumber,
            }).ToList();

            return new BaseResponse<ICollection<AuthorResponseModel>>
            {
                Data = authors,
                Message = "List of authors",
                IsSucessful = true
            };
        }

        public BaseResponse<AuthorResponseModel> GetAuthor(Guid id)
        {
            var getAuthor = _authorRepository.GetAuthorById(id);
            if (getAuthor == null)
            {
                return new BaseResponse<AuthorResponseModel>
                {
                    Message = "Author not found",
                    IsSucessful = false,
                };
            }

            var author = new AuthorResponseModel
            {
                Id = getAuthor.Id,

                Username = $"{getAuthor.FirstName}{getAuthor.LastName}",

                Email = getAuthor.Email,

                Gender = getAuthor.Gender,

                Address = getAuthor.Address,

                PhoneNumber = getAuthor.PhoneNumber,

            };

            return new BaseResponse<AuthorResponseModel>
            {
                Data = author,
                Message = "Author information",
                IsSucessful = true
            };
        }

        public BaseResponse UpdateAuthor(Guid id, AuthorUpdateRequestModel authorUpdateRequest)
        {
            var getAuthor = _authorRepository.GetAuthorById(id);
            if(getAuthor == null)
            {
                return new BaseResponse
                {
                    Message = "Author not found",
                    IsSucessful = false,
                };
            }

            var author = new Author();

            author.FirstName = getAuthor.FirstName; 

            author.LastName = getAuthor.LastName;

            author.Email = getAuthor.Email;

            author.Address = getAuthor.Address;

            author.PhoneNumber = getAuthor.PhoneNumber;

            _authorRepository.UpdateAuthor(author);

            _authorRepository.Save();

            return new BaseResponse
            {
                Message = "Author profile updated",
                IsSucessful = false,
            };
        }
    }
}
