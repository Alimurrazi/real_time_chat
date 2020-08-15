using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Domain.Services;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IIdentityService _iidentityService;

        public UserController(IIdentityService iidentityService)
        {
            _iidentityService = iidentityService;
        }

        [HttpGet("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var response = await _iidentityService.GetUserById(userId);
            return Ok(response);
        }

        [HttpPost("getUserByValue")]
        public async Task<IActionResult> GetUserByValue([FromBody] )
        {

        }
    }
}