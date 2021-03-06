﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Responses;

namespace server.Domain.Services
{
    public interface IUserService
    {
        Task<BaseResponse> GetUserById(string userId);
        Task<BaseResponse> GetUserByValue(dynamic key, dynamic value);
        Task<BaseResponse> UpdateUser(User user);
        Task<BaseResponse> ChangePassword(PasswordChangeData passwordChangeData, string userId);
        Task<BaseResponse> GetUsers(int pageNumber, int pageSize);
    }
}
