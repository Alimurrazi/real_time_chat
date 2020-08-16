using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Services;
using server.Domain.Repositories;
using server.Responses;

namespace server.Services
{
    public class UserService: IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> GetUserById(string userId)
        {
            try
            {
                User user = await _userRepository.GetUserById(userId);
                //            User user = await _userRepository.GetUserByValue("Id", userId);
                return new BaseResponse(true, null, user);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public async Task<BaseResponse> GetUserByValue(dynamic key, dynamic value)
        {
            try
            {
                var users = await _userRepository.GetUserByValue(key, value);
                return new BaseResponse(true, null, users);
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
    }
}
