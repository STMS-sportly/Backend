using Core.Models;
using FirebaseAdmin.Auth;
using Logic.User;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Core.Controllers
{
    [ApiController]
    [Route("team/[action]")]
    public class TeamController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TokenFirebase tokenId)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenId.idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logic = new UserLogic(context);
                var userExists = logic.UserExist(user);
                if (!userExists)
                {
                    logic.AddUser(user);
                    logic.Save();
                    return Ok("Add New User");
                }
                else
                {
                    var teams = logic.GetUserTeams(user);
                    return Ok(Json(teams));
                }
            }
            catch (FirebaseAuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
