using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class BaseController : Controller
    {
        protected StmsContext? Context => HttpContext.RequestServices.GetService(typeof(StmsContext)) as StmsContext;
    }
}
