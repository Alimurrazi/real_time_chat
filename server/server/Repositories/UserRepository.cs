using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using server.Domain.Models;
using server.Domain.Repositories;

namespace server.Repositories
{
    public class UserRepository: BaseRepository, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IDatabaseSettings settings):base(settings)
        {
            _users = (IMongoCollection<User>)_database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<List<User>> GetUserByValue(dynamic key, dynamic value)
        {
            key = Convert.ToString(key);
            value = Convert.ToString(value);
            var Filter = new BsonDocument(key, value);
            var userList = await _users.Find(Filter).ToListAsync();
            return userList;
        }

        public async Task<User> GetUserByCredential(string mail, string password)
        {
            var user = await _users.Find(user => user.Mail == mail && user.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetUsers(int pageNumber, int pageSize)
        {
            var userList = await _users.Find(user => true).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
            return userList;
        }

        public async Task UpdateUser(User updatedUser)
        {
            await _users.ReplaceOneAsync(user => user.Id == updatedUser.Id, updatedUser);
        }

        public async Task ChangePassword(string password, string userId)
        {
            var filter = Builders<User>.Filter.Eq("Id", userId);
            var update = Builders<User>.Update.Set("Password", password);
            _users.UpdateOne(filter, update);
        }
    }
}
