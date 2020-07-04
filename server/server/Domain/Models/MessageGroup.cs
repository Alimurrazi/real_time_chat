using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Models
{
    public class MessageGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Members { get; set; }
        public string Admins { get; set; }
    }
}
