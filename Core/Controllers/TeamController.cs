using Core.DTOs;
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
        public async Task<ActionResult> CreateTeam([FromHeader]string idToken, CreateTeamDTO newTeam)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);

                var logic = new TeamLogic(Context);
                //logic.CreateTeam(user, newTeam);
                logic.Save();
                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return BadRequest(ex.Source);
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
                    return Json(new List<GetTeamsDTO>());
                }
                else
                {
                    var userTeamsId = logic.GetUsersTeams(user.Email);
                    var logicTeam = new TeamLogic(Context);
                    var teamsData = logicTeam.GetTeams(userTeamsId);
                    return Json(teamsData);
                }
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetTeamDetails([FromHeader] string idToken, [FromHeader] Guid teamId)
        {
            // TODO
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetDisciplines([FromHeader] string idToken, [FromHeader] Guid teamId)
        {
            // TODO
            return Ok();
        }
    }
}
