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
using server.Storages;
using server.Responses;

namespace server.Controllers
{
    [Route("api/message")]
    [Authorize]
    public class MessageController : Controller
    {
        private IHubContext<MessageHub> _messageHubContext;
        private readonly IMessageService _messageService;
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public MessageController(IHubContext<MessageHub> MessageHubContext, IMessageService messageService){
            _messageHubContext = MessageHubContext;
            _messageService = messageService;
        }

        [HttpGet("getActiveUsers")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var res = _connections.UserList();
            var result = new BaseResponse(true, null, res);
            return Ok(result);
        }

        [HttpPost]
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
    }
}