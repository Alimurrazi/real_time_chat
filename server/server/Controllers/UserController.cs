using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Domain.Services;
using server.Domain.Models;

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
    }
}