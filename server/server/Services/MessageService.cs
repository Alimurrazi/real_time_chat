using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;
using server.Domain.Repositories;
using server.Domain.Services;
using server.Responses;
using server.Storages;
using server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace server.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageService(IMessageRepository messageRepository, IHubContext<MessageHub> hubContext)
        {
            _messageRepository = messageRepository;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse> SendMessageAsync(Message message)
        {
            try
            {
                message.Id = Guid.NewGuid().ToString();
                await _messageRepository.SaveMessageAsync(message);

                List<string> ReceiverConnectionIds = _connections.GetConnections(message.ReceiverId).ToList<string>();
                if (ReceiverConnectionIds.Count() > 0)
                {
                //    await _hubContext.Clients.All.SendAsync();
                    await _hubContext.Clients.Clients(ReceiverConnectionIds).SendAsync("ReceiveMessage", message);
                }

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
