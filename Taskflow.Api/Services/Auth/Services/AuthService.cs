using Microsoft.EntityFrameworkCore;
using Taskflow.Api.Data;
using Taskflow.Api.DTOs.Auth;
using Taskflow.Api.Models;

namespace Taskflow.Api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly TaskflowDbContext _db;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtService _jwt;

        public AuthService(TaskflowDbContext db, IPasswordHasher hasher, IJwtService jwt)
        {
            _db = db;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            if (await _db.users.AnyAsync(u => u.Email == request.Email))
                return false;

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _hasher.Hash(request.Password)
            };

            _db.users.Add(user);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            var user = await _db.users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                return null;

            return _jwt.GenerateToken(user.Id, user.FullName, user.Email);
        }
    }
}