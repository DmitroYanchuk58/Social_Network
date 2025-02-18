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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet("Login")]
        [ProducesResponseType(typeof(object), 200)] 
        [ProducesResponseType(typeof(object), 401)]
        public IActionResult Login(string email, string password)
        {
            var isAuthenticated = _service.Authentication(email, password);
            if (isAuthenticated)
            {
                var token = JwtTokenGenerator.GenerateToken(email);
                return Ok(new { token });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpPost("Registration")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 401)]
        public IActionResult Registration(string email, string password, string nickname)
        {
            try
            {
                _service.Registration(email, password, nickname);
                var token = JwtTokenGenerator.GenerateToken(email);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred during registration", Details = ex.Message });
            }
        }
    }
}
