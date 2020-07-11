using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Domain.Models;
using server.Extensions;
using server.Hubs;
using server.Resources;
using System.Threading.Tasks;
using server.Services;
using server.Domain.Services;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private IHubContext<MessageHub> _messageHubContext;
        private readonly IMessageService _messageService;

        public MessageController(IHubContext<MessageHub> MessageHubContext, IMessageService messageService){
            _messageHubContext = MessageHubContext;
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var result = await _messageService.SendMessageAsync(message);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        //[HttpGet]
        //public IActionResult Get(){
        //    _messageHubContext.Clients.All.SendAsync("send","Hello from server");
        //    return Ok();
        //}
    }
}