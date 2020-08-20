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
        public async Task<IActionResult> UpdateUser([FromBody] dynamic user)
        {
            string dynamicUser = JsonSerializer.Serialize(user);
            //Type type = user.GetType();
            //PropertyInfo[] properties = type.GetProperties();

          //  object dynamicUser = user;
          //  string[] propertyNames = o.GetType().GetProperties().Select(p => p.Name).ToArray();

            Type type = dynamicUser.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.GetValue(dynamicUser, null));
                Console.WriteLine(property.Name);
            }


            var response = await _userService.UpdateUser(user);
            return Ok(response);
        }

        [HttpPost("changePassword")]

        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeData passwordChangeData )
        {

            var principal = HttpContext.User;
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            //var userId = HttpContext.User.Claims.First(claim => claim.Type == "NameIdentifier").Value;
            var response = await _userService.ChangePassword(passwordChangeData, userId);
            return Ok(response);
        }
    }
}