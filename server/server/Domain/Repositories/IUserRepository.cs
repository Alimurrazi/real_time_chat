using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using server.Domain.Models;

namespace server.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<List<User>> GetUserByValue(dynamic key, dynamic value);
        Task<User> GetUserByCredential(string mail, string password);
        Task<User> GetUserById(string userId);
        Task UpdateUser(User updatedUser);
        Task ChangePassword(string password, string userId);
        Task<List<User>> GetUsers(int pageNumber, int pageSize);
    }
}
