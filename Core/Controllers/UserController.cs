using Core.Models;
using Data.DataAccess;
using FirebaseAdmin.Auth;
using Logic.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> GetTeam([FromBody] TokenFirebase tokenId)
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
