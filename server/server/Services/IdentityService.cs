using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Services;
using server.Domain.Repositories;
using server.Domain.Security;
using MongoDB.Driver;
using server.Repositories;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using server.Responses;
using server.Security;
using server.Domain.Security.IPasswordHasher;
using server.Resources;

namespace server.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public IdentityService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
        }

        public async Task<BaseResponse> CreateUserAsync(User user)
        {
            try
            {
                var mailCheckResponse = await IsEmailExistsAsync(user.Mail);
                if (mailCheckResponse.IsSuccess == false)
                {
                    return mailCheckResponse;
                }
              //  user.Password = this.GetHashedPassword(user.Password);
                user.Password = _passwordHasher.GetHashedPassword(user.Password);
                user.Id = Guid.NewGuid().ToString();
                await _userRepository.CreateAsync(user);
                return new BaseResponse(true, null, null);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        private BaseResponse GetErrorResponse(string msg)
        {
            List<string> errorMsg = new List<string>();
            errorMsg.Add(msg);
            return new BaseResponse(false, errorMsg, null);
        }

        public async Task<BaseResponse> CreateAccessTokenAsync(string mail, string password)
        {
            try{
                string hashedPassword = _passwordHasher.GetHashedPassword(password);
                User user = await _userRepository.GetUserByCredential(mail, hashedPassword);
                if (user == null)
                {
                    return GetErrorResponse("Invalid credentials");
                }

                var token = _tokenHandler.CreateAccessToken(user);

                return new BaseResponse(true, null, token);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public async Task<BaseResponse> IsEmailExistsAsync(string mail)
        {
            try
            {
                var users = await _userRepository.GetUserByValue("Mail", mail);
                if (users.Count == 0)
                {
                    return new BaseResponse(false, null, null);
                }
                else
                {
                    return GetErrorResponse("Email already exists");
                }
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public async Task<BaseResponse> RefreshTokenAsync(RefreshTokenResource refreshTokenResource)
        {
            try
            {
                var token = _tokenHandler.TakeRefreshToken(refreshTokenResource.token);
                if (token == null)
                {
                    return GetErrorResponse("Invalid refresh token");
                }
                if (token.IsExpired())
                {
                    return GetErrorResponse("Expired refresh token");
                }
                User user = await _userRepository.GetUserByValue("Id", refreshTokenResource.userId);
                if (user == null)
                {
                    return GetErrorResponse("Invalid refresh token");
                }
                var accesstoken = _tokenHandler.CreateAccessToken(user);

                return new BaseResponse(true, null, accesstoken);
            }
            catch(Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public Task<BaseResponse> RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
