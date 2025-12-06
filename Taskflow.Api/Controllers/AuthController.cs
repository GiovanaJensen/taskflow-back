using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskflow.Api.Data;
using Taskflow.Api.Models;
using Taskflow.Api.Services;

namespace Taskflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskflowDbContext _db;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtService _jwt;

        public AuthController(TaskflowDbContext db, IPasswordHasher hasher, IJwtService jwt)
        {
            _db = db;
            _hasher = hasher;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _db.users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Email já cadastrado.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _hasher.Hash(request.Password)
            };

            _db.users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _db.users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Credenciais inválidas.");

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Credenciais inválidas.");

            var token = _jwt.GenerateToken(user.Id, user.FullName, user.Email);

            return Ok(new { token });
        }
    }

    public record RegisterRequest(string FullName, string Email, string Password);
    public record LoginRequest(string Email, string Password);
}