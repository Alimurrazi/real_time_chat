using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using server.Storages;
using server.Domain.Models;
using System.Collections.Generic;

namespace server.Hubs
{
    [Authorize]
    public class MessageHub: Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        public async Task Send(Message message){
            if (message!=null)
            {
                List<string> ReceiverConnectionIds = _connections.GetConnections(message.ReceiverId).ToList<string>();
                if (ReceiverConnectionIds.Count() > 0)
                {
                    try
                    {
                        await Clients.Clients(ReceiverConnectionIds).SendAsync("ReceiveMessage", message);
                    }
                    catch (Exception ex)
                    {
                        _ = ex;
                    }
                }
            }
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                try
                {
                    var principal = httpContext.User;
                    var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    _connections.Add(userId, Context.ConnectionId);
                    await Clients.All.SendAsync("UpdaedUserList", _connections.ToJson());
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                try
                {
                    var principal = httpContext.User;
                    var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    _connections.Remove(userId, Context.ConnectionId);
                    await Clients.All.SendAsync("UpdaedUserList", _connections.ToJson());
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
        }

    }
}