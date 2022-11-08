using Core.Models;
using FirebaseAdmin.Auth;
using Logic.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> GetUserTeams([FromBody] TokenFirebase tokenId)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenId.idToken);

            string userid = decodedToken.Uid;

            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);


            return this.Ok();
        }
    }
}
