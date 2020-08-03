using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Extensions;
using server.Resources;
using server.Domain.Services;
using server.Domain.Security;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IIdentityService _iidentityService;
        public AuthController(IIdentityService iidentityService)
        {
            _iidentityService = iidentityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]UserCredentialResource userCredentialResource)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }


            var response = await _iidentityService.CreateAccessTokenAsync(userCredentialResource.Mail, userCredentialResource.Password);

            return Ok(response);
        }

        [HttpPost("token/refresh")]

        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _iidentityService.RefreshTokenAsync(refreshTokenResource);

            return Ok(response);
        }

        [HttpGet("getUserById")]

        public async Task<IActionResult> GetUserById(string userId)
        {
            var response = await _iidentityService.GetUserById(userId);
            return Ok(response);
        }
    }
}