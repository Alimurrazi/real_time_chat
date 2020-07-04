using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using server.Domain.Models;

namespace server.Repositories
{
    public class BaseRepository
    {
        public readonly IMongoDatabase _database;
        public BaseRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
