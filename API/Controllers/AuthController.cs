using Application.DTOs;
using Asp.Versioning;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // POST: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            // Dummy Login (Replace with Database Validation)
            if (login.Username == "admin" &&
                login.Password == "admin123")
            {
                var accessToken = _jwtService.GenerateToken(login.Username);

                var refreshToken = Guid.NewGuid().ToString();

                return Ok(new
                {
                    accessToken,
                    refreshToken,
                    expiresIn = 3600
                });
            }

            return Unauthorized(new
            {
                message = "Invalid username or password"
            });
        }

        // POST: api/Auth/refresh-token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return BadRequest(new
                {
                    message = "Refresh Token is required."
                });
            }

            // Dummy Refresh Token Logic
            var newAccessToken = _jwtService.GenerateToken("admin");
            var newRefreshToken = Guid.NewGuid().ToString();

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken,
                expiresIn = 3600
            });
        }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}