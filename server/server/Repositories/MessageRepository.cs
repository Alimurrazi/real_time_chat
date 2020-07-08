using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using server.Domain.Models;
using server.Domain.Repositories;

namespace server.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(IDatabaseSettings settings):base(settings)
        {
            _messages = _database.GetCollection<Message>(settings. MessageCollection);
        }
        public async Task SaveMessageAsync(Message message)
        {
            await _messages.InsertOneAsync(message);
           // throw new NotImplementedException();
        }
    }
}
