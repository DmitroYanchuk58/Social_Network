using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("ChangeName")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
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
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)] 
        [ProducesResponseType(typeof(string), 401)]
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
        [ProducesResponseType(typeof(BAL.DTOs.User), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetAccount(Guid id)
        {
            var user = _service.GetUser(id);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("MyAccount")]
        [ProducesResponseType(typeof(BAL.DTOs.User), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult MyAccount(Guid id)
        {
            //Add taking id from JWT token
            var user = _service.GetUser(id);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("DeleteAccount")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)] 
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult DeleteAccount(Guid id)
        { 
            //Add checking id == id from JWT token
            _service.DeleteUser(id);
            return Ok();
        }
    }
}
