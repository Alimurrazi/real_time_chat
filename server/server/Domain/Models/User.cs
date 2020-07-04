using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace server.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ProfielImg { get; set; }

        public static implicit operator User(List<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
