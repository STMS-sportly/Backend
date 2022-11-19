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
                // logic.CreateTeam(newTeam.GetNewTeam(user.Email));
                // logic.Save();
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
                    logic.Save();
                    return Json(new List<GetTeamsDTO>());
                }
                else
                {
                    var teamLogic = new TeamLogic(Context);
                    var teams = teamLogic.GetUserTeams(user);
                    List<GetTeamsDTO> teamsList = new List<GetTeamsDTO>();
 
                    foreach(var team in teams)
                    {
                        teamsList.Add(new GetTeamsDTO()
                        {
                            Id = team.TeamId,
                            TeamName = team.TeamName,
                            Discipline = new DisciplineName()
                            {
                                Name = "" // TODO
                            },
                            IsAdmin = false, // TODO
                            MembersCount = 0, // TODO
                        });
                    }
                    return Json(teams);
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
