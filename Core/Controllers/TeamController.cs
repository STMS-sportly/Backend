using Data.Enums;
using Data.Models;
using FirebaseAdmin.Auth;
using Logic.ALL.DTOs;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.Controllers
{
    [ApiController]
    [Route("team/[action]")]
    public class TeamController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromHeader] string idToken, CreateTeamDTO newTeam)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logic = new TeamLogic(Context);
                logic.CreateTeam(user, newTeam);
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
        public async Task<ActionResult?> GetTeams([FromHeader] string idToken)
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
                    return Json(new { Teams = new List<GetTeamsDTO>() });
                }
                else
                {
                    var userTeamsId = logic.GetUsersTeams(user.Email);
                    var logicTeam = new TeamLogic(Context);
                    var teams = logicTeam.GetTeams(userTeamsId);
                    return Json(new { Teams = teams });
                }
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public async Task<ActionResult?> GetDisciplines([FromHeader] string idToken)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                List<GetDesciplinesDTO> allDisciplines = new List<GetDesciplinesDTO>();
                foreach (var discipline in Enum.GetNames(typeof(EDiscipline)))
                {
                    allDisciplines.Add(new GetDesciplinesDTO
                    {
                        Name = discipline
                    });
                }

                return Json(new { disciplines = allDisciplines });
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult?> GetTeamDetails([FromHeader] string idToken, [FromRoute] int teamId)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logicTeam = new TeamLogic(Context);
                var teamsData = logicTeam.GetTeamDetails(user.Email, teamId);
                return Json(teamsData);
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult?> GetTeamCode([FromHeader] string idToken, [FromRoute] int teamId)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                var teamLogic = new TeamLogic(Context);
                GetTeamCodeDTO teamCode = teamLogic.GetTeamCode(teamId);

                return Json(teamCode);
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult?> JoinTeam([FromHeader] string idToken, JoinTeamDTO codeTeam)
        {


            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var teamLogic = new TeamLogic(Context);
                bool response = teamLogic.JoinTeam(user.Email, codeTeam.Code);

                if (!response)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return null;
            }
        }

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> DeleteTeam([FromHeader] string idToken, [FromRoute]int teamId)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                var teamLogic = new TeamLogic(Context);
                teamLogic.DeleteTeam(teamId);
                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return BadRequest();
            }
            catch(Exception ex)
            {
                var logs = new LogsLogic(Context);
                logs.AddLog(ex.Message);
                return BadRequest();
            }
        }

    }
}
