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
            _users = _database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<List<User>> GetUserByValue(string key, string value)
        {

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
    }
}
