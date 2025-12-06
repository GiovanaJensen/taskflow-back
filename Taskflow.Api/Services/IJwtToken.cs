namespace Taskflow.Api.Services
{
    public interface IJwtService
    {
        string GenerateToken(long userId, string fullname, string email);
        DateTime GetExpiry();
    }
}