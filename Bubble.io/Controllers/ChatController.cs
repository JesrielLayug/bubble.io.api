using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bubble.io.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChatController(IChatService chatService, IHttpContextAccessor httpContextAccessor)
        {
            this.chatService = chatService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Send(string recieverId, string content)
        {
            try
            {
                var senderId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var message = new DTOChatMessageRequest
                {
                    senderId = senderId,
                    receiverId = recieverId,
                    content = content
                };

                await chatService.Send(message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string receiverId)
        {
            try
            {
                var senderId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var messages = await chatService.Get(senderId, receiverId);

                return new OkObjectResult(messages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
