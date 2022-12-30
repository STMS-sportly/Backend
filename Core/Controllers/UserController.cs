using Data.DTOs;
using Firebase.Auth;
using Logic.ALL.UserAuthorization;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("user/[action]")]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetUserData([FromHeader] string idToken)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new UserLogic(Context);
                var userData = logic.GetUserData(user.Email);
                return Json(userData);
            }
            catch (FirebaseAuthException ex)
            {
                return Unauthorized(AddLog(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(AddLog(ex));
            }
        }
    }
}
