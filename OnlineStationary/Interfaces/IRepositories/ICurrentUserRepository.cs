namespace OnlineStationary.Interfaces.IRepositories
{
    public interface ICurrentUserRepository
    {
        string GetCurrentUser();
        string GetCurrentUserId();
    }
}
