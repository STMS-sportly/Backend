using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class BaseController : Controller
    {
#pragma warning disable CS8603 // Possible null reference return.
        protected StmsContext Context => HttpContext.RequestServices.GetService(typeof(StmsContext)) as StmsContext;
#pragma warning restore CS8603 // Possible null reference return.
    }
}
