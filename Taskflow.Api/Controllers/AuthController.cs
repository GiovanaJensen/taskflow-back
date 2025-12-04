using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Taskflow.Api.DTOs;
using Taskflow.Api.Repositories.Interfaces;
using Taskflow.Api.Services;

namespace Taskflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;


        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOs.LoginRequest request)
        {
            var user = _userRepository.GetByUsername(request.Username);
            if (user == null) return Unauthorized(new { message = "Usuário não encontrado" });


            // Em produção: verifique hash da senha, não texto puro
            if (user.Password != request.Password) return Unauthorized(new { message = "Senha inválida" });


            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);


            return Ok(new LoginResponse { Token = token, ExpiresAt = _jwtService.GetExpiry() });
        }
    }
}