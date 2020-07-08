using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Repositories;
using server.Domain.Services;
using server.Responses;

namespace server.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<BaseResponse> SendMessageAsync(Message message)
        {
            try
            {
                message.Id = Guid.NewGuid().ToString();
                await _messageRepository.SaveMessageAsync(message);
                return new BaseResponse(true, null, null);
            }
            catch(Exception ex)
            {
                return GetErrorResponse(ex.Message);
            }
        }

        private BaseResponse GetErrorResponse(string msg)
        {
            List<string> errorMsg = new List<string>();
            errorMsg.Add(msg);
            return new BaseResponse(false, errorMsg, null);
        }
    }
}
