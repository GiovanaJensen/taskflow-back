using Taskflow.Api.DTOs.Auth;

namespace Taskflow.Api.Services.Auth
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
