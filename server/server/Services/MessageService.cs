using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Services;
using server.Responses;

namespace server.Services
{
    public class MessageService : IMessageService
    {
        public Task<BaseResponse> SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }

        private BaseResponse GetErrorResponse(string msg)
        {
            List<string> errorMsg = new List<string>();
            errorMsg.Add(msg);
            return new BaseResponse(false, errorMsg, null);
        }
    }
}
