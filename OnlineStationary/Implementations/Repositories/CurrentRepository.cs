using OnlineStationary.Interfaces.IRepositories;
using System.Security.Claims;

namespace OnlineStationary.Implementations.Repositories
{
    public class CurrentRepository:ICurrentUserRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentRepository(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetCurrentUser()
        {
            try
            {
                var httpContext = _contextAccessor.HttpContext;
                var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email);

                return emailClaim.Value;
            }
            catch (Exception) {
                throw new InvalidOperationException("Email claim not found.");
            }
        }

        public string GetCurrentUserId()
        {

            try
            {
                var httpContext = _contextAccessor.HttpContext;
                var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                return idClaim.Value;
            }

            catch (Exception)
            {
                throw new InvalidOperationException("Id claim not found.");
            }
        }
    }
}
