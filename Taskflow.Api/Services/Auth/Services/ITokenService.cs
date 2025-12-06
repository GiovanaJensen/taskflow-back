using Taskflow.Api.DTOs.Auth;

namespace Taskflow.Api.Services.Auth
{
    public interface ITokenService
    {
        void RevokeToken(string token);
        bool IsTokenRevoked(string token);
    }
}
