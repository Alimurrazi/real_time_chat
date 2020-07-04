using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Security;
using server.Responses;
using server.Resources;

namespace server.Domain.Services
{
    public interface IIdentityService
    {
        Task<BaseResponse> CreateUserAsync(User user);
        Task<BaseResponse> IsEmailExistsAsync(string mail);
        Task<BaseResponse> CreateAccessTokenAsync(string mail, string password);
        Task<BaseResponse> RefreshTokenAsync(RefreshTokenResource refreshTokenResource);
        Task<BaseResponse> RevokeToken(string token);
    }
}
