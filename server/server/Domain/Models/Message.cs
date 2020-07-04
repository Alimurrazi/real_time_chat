using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsGroupMsg { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
