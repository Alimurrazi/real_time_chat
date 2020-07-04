using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace server.Hubs
{
    public class MessageHub: Hub
    {
        public async Task Send(string msg){
             await Clients.All.SendAsync("send", msg);
        }       
    }
}