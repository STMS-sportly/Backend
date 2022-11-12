using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class BaseController : Controller
    {
        protected STMSContext context => HttpContext.RequestServices.GetService(typeof(STMSContext)) as STMSContext;
    }
}
