using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskflow.Api.DTOs.Auth;
using Taskflow.Api.Services.Auth;

namespace Taskflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var success = await _authService.RegisterAsync(request);

            if (!success)
                return BadRequest("Email já cadastrado.");

            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);

            if (token == null)
                return Unauthorized("Credenciais inválidas.");

            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "");

            _tokenService.RevokeToken(token);

            return Ok(new { message = "Logout realizado com sucesso." });
        }

    }

}