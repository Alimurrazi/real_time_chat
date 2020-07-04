using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Responses;
using server.Domain.Models;

namespace server.Domain.Services
{
    interface IMessageService
    {
       public Task<BaseResponse> SendMessageAsync(Message message);
    }
}