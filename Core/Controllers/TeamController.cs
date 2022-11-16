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
        public async Task<ActionResult> CreateTeam([FromHeader]string tokenId, TeamTO newTeam)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenId);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);

                var logic = new TeamLogic(Context);
                newTeam.Email = user.Email;
                logic.CreateTeam(newTeam.GetNewTeam());
                logic.Save();
                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
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
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
