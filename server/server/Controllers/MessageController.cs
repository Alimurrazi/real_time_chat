using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;

namespace server.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private IHubContext<MessageHub> _messageHubContext;

        public MessageController(IHubContext<MessageHub> MessageHubContext){
            _messageHubContext = MessageHubContext;
        }
        public IActionResult Get(){
            _messageHubContext.Clients.All.SendAsync("send","Hello from server");
            return Ok();
        }
    }
}