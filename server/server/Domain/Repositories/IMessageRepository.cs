using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;

namespace server.Domain.Repositories
{
   public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
    }
}
