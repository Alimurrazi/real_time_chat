using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace server.Hubs
{
    public class MessageHub: Hub
    {
        //private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        public async Task Send(string msg){
             await Clients.All.SendAsync("send", msg);
        }

        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            //if (httpContext != null)
            //{
            //    try
            //    {
            //        //Add Logged User
            //        var userName = httpContext.Request.Query["user"].ToString();
            //        //var UserAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault().ToString();
            //        var connId = Context.ConnectionId.ToString();
            //        _connections.Add(userName, connId);

            //        //Update Client
            //        await Clients.All.SendAsync("UpdateUserList", _connections.ToJson());
            //    }
            //    catch (Exception) { }
            //}
        }

    }
}