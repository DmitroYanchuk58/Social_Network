using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_PL.Helpers;
using BAL.DTOs;

namespace WebApp_PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("ChangeName")]
        public IActionResult ChangeNickname(Guid id, string newNickname)
        {
            if (string.IsNullOrEmpty(newNickname))
            {
                throw new ArgumentNullException(nameof(newNickname));
            }
            _service.ChangeNickname(id, newNickname);
            return Ok();
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(Guid id, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException(nameof(newPassword));
            }
            _service.ChangePassword(id, newPassword);
            return Ok();
        }

        [HttpGet("GetAccount")]
        public IActionResult GetAccount(Guid id)
        {
            var user = _service.GetUser(id);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("MyAccount")]
        public IActionResult MyAccount(Guid id)
        {
            //Add taking id from JWT token
            var user = _service.GetUser(id);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("DeleteAccount")]
        public IActionResult DeleteAccount(Guid id)
        { 
            //Add checking id == id from JWT token
            _service.DeleteUser(id);
            return Ok();
        }
    }
}
