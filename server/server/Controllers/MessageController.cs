using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Domain.Models;
using server.Extensions;
using server.Hubs;
using server.Resources;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private IHubContext<MessageHub> _messageHubContext;

        public MessageController(IHubContext<MessageHub> MessageHubContext){
            _messageHubContext = MessageHubContext;
        }

        public async Task<IActionResult> PostAsync([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }



            return Ok();
        }
        public IActionResult Get(){
            _messageHubContext.Clients.All.SendAsync("send","Hello from server");
            return Ok();
        }
    }
}