using Taskflow.Api.Models;

namespace Taskflow.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
    }
}