using BAL.Services;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp_PL.Helpers;

namespace WebApp_PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            var isAuthenticated = _service.Authentication(email, password);
            if (isAuthenticated)
            {
                var token = JwtTokenGenerator.GenerateToken(email, _configuration);
                return Ok(new { token });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpPost("Registration")]
        public IActionResult Registration(string email, string password, string nickname)
        {
            try
            {
                _service.Registration(email, password, nickname);
                var token = JwtTokenGenerator.GenerateToken(email, _configuration);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred during registration", Details = ex.Message });
            }
        }
    }
}
