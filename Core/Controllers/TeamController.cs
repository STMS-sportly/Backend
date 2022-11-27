using Core.DTOs;
using Data.Enums;
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
                var team =  new Team()
                {
                    TeamName = newTeam.TeamName,
                    TeamType = (int)(ETeamType)Enum.Parse(typeof(ETeamType), newTeam.TeamType),
                    SportType = (int)(EDiscipline)Enum.Parse(typeof(EDiscipline), newTeam.Discipline.Name),
                    Location = newTeam.Location,
                    OrganizationName = newTeam.OrganizationName
                };
                logic.CreateTeam(user, team);
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
                    return Json(new List<object>());
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
        public async Task<ActionResult> GetDisciplines([FromHeader] string idToken)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

                List<object> allDisciplines = new List<object>();

                foreach (var discipline in Enum.GetNames(typeof(EDiscipline)))
                {
                    allDisciplines.Add(new
                    {
                        Name = discipline
                    });
                }

                return Json(allDisciplines);
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return Unauthorized(ex.Message);
            }
        }
    }
}
