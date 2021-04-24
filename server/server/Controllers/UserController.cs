using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Domain.Services;
using server.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Reflection;
using System.Text.Json;
using System.Dynamic;

namespace server.Controllers
{
    [Route("api/[controller]")]

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
            string serializedObject = JsonSerializer.Serialize(user);
            User updatedUser = JsonSerializer.Deserialize<User>(serializedObject);

            var response = await _userService.UpdateUser(updatedUser);
            return Ok(response);
        }

        [HttpPost("changePassword")]

        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeData passwordChangeData )
        {

            var principal = HttpContext.User;
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _userService.ChangePassword(passwordChangeData, userId);
            return Ok(response);
        }

        [HttpGet("getUsers")]

        public async Task<IActionResult> GetUsers(int pageNumber, int pageSize)
        {
            var response = await _userService.GetUsers(pageNumber, pageSize);
            return Ok(response);
        }
    }
}