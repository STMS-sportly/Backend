using Core.Models;
using Data.Models;
using FirebaseAdmin.Auth;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("team/[action]")]
    public class TeamController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromHeader]string tokenId, Team newTeam)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenId);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                if (Context == null) { 
                    return BadRequest("Problem With Context"); 
                }
                var logic = new TeamLogic(Context);
                logic.CreateTeam(newTeam, user);
                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTeams([FromHeader] string idToken)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                if (Context == null)
                {
                    return BadRequest("Problem With Context");
                }

                var logic = new UserLogic(Context);
                var userExists = logic.UserExist(user);
                if (!userExists)
                {
                    logic.AddUser(user);
                    logic.Save();
                    return Json(new List<TeamTO>());
                }
                else
                {
                    var teamLogic = new TeamLogic(Context);
                    Teams teams = new (teamLogic.GetUserTeams(user));
                    return Json(teams);
                }
            }
            catch (FirebaseAuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
