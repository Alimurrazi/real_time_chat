using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Services;
using server.Domain.Repositories;
using server.Responses;
using server.Security.Hashing;
using server.Domain.Security.IPasswordHasher;
using System.Reflection;

namespace server.Services
{
    public class UserService: IUserService
    {
        private IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<BaseResponse> GetUserById(string userId)
        {
            try
            {
                User user = await _userRepository.GetUserById(userId);
                return new BaseResponse(true, null, user);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public async Task<BaseResponse> GetUsers(int pageNumber, int pageSize)
        {
            try
            {
                var users = await _userRepository.GetUsers(pageNumber, pageSize);
                return new BaseResponse(true, null, users);
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

        public async Task<BaseResponse> UpdateUser(User updatedUser)
        {
            try
            {
                User existedUser = await _userRepository.GetUserById(updatedUser.Id);
                Type type = updatedUser.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    var propertyName = property.Name;
                    var updatedUserValue = property.GetValue(updatedUser, null);
                    var existedUserValue = property.GetValue(existedUser, null);

                    if(updatedUserValue == null) {
                        property.SetValue(updatedUser, existedUserValue, null);
                    };
                }

                await _userRepository.UpdateUser(updatedUser);
                return new BaseResponse(true, null, null);
            }
            catch(Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        public async Task<BaseResponse> ChangePassword(PasswordChangeData passwordChangeData, string userId)
        {
            try
            {
                User user = await _userRepository.GetUserById(userId);
                if(_passwordHasher.GetHashedPassword(passwordChangeData.oldPassword) == user.Password)
                {
                    try
                    {
                        var newPassword = _passwordHasher.GetHashedPassword(passwordChangeData.newPassword);
                        await _userRepository.ChangePassword(newPassword, userId);
                        return new BaseResponse(true, null, null);
                    }
                    catch(Exception ex)
                    {
                        return GetErrorResponse(ex.Message);
                    }
                }
                else
                {
                    return GetErrorResponse("Old password not match");
                }
            }catch(Exception ex)
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
