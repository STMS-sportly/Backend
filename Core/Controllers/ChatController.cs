using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("chat/[action]")]
    public class ChatController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> SendMessage()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetMessages()
        {
            return Ok();
        }
    }
}
