using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Domain.Services;
using server.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private IIdentityService _iidentityService;
        private IUserService _userService;

        public UserController(IIdentityService iidentityService, IUserService userService)
        {
            _iidentityService = iidentityService;
            _userService = userService;
        }

        [HttpGet("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var response = await _iidentityService.GetUserById(userId);
            return Ok(response);
        }

        [HttpPost("getUserByValue")]
        public async Task<IActionResult> GetUserByValue([FromBody] KeyValue keyValue)
        {
            var response = await _userService.GetUserByValue(keyValue.key, keyValue.value);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var response = await _userService.UpdateUser(user);
            return Ok(response);
        }

        [HttpPost("changePassword")]

        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeData passwordChangeData )
        {
            var userId = HttpContext.User.Claims.First(claim => claim.Type == "NameIdentifier").Value;
            var response = await _userService.ChangePassword(passwordChangeData, userId);
            return Ok(response);
        }
    }
}