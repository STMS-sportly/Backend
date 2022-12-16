using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("chat/[action]")]
    public class ChatController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
