using BAL.Services;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        IUserService _service;
        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login(string email, string password)
        {
            var isAuthenticated = _service.Authentication(email, password);
            if (isAuthenticated)
            {
                return Ok(new { Message = "Login successful" });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpPost]
        public IActionResult Registration(string email, string password, string nickname)
        {
            try
            {
                _service.Registration(email, password, nickname);
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred during registration", Details = ex.Message });
            }
        }
    }
}
