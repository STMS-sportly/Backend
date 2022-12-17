using Data.DataAccess;
using FirebaseAdmin.Auth;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class BaseController : Controller
    {
#pragma warning disable CS8603 // Possible null reference return.
        protected StmsContext Context => HttpContext.RequestServices.GetService(typeof(StmsContext)) as StmsContext;
#pragma warning restore CS8603 // Possible null reference return.

        protected string AddLog<T>(T ex) where T : Exception
        {
            var logs = new LogsLogic(Context);
            logs.AddLog(ex.Message);
            return ex.Message;
        }
    }
}
