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

        ////[Authorize]
        //[HttpPost("EditAccount")]
        //public IActionResult EditAccount(User user)
        //{
        //    _service.UpdateUser(user.Id, user);
        //    return Ok();
        //}

        //[HttpPost("ChangeName")]
        //public IActionResult ChangeNickname(Guid id, string newNickname)
        //{
        //    User user = _service.GetUser(id);
        //    user.Nickname = newNickname;
        //    _service.UpdateUser(id, user);
        //    return Ok();
        //}

        //[HttpGet("GetAccount")]
        //public IActionResult GetAccount(Guid id)
        //{
        //    var user = _service.GetUser(id);
        //    return Ok(user);
        //}

        //[Authorize]
        //[HttpGet("MyAccount")]
        //public IActionResult MyAccount(Guid id)
        //{
        //    var user = _service.GetUser(id);
        //    return Ok(user);
        //}

        //[Authorize]
        //[HttpPost("DeleteAccount")]
        //public IActionResult DeleteAccount(Guid id)
        //{
        //    _service.DeleteUser(id);
        //    return Ok();
        //}
    }
}
