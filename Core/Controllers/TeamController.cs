using Data.DTOs;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using FirebaseAdmin.Auth;
using Logic.ALL.UserAuthorization;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Authentication;

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
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new TeamLogic(Context);
                logic.CreateTeam(user, newTeam);
                return Ok();
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

        [HttpGet]
        public async Task<ActionResult?> GetTeams([FromHeader] string idToken)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
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
                return Unauthorized(AddLog(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(AddLog(ex));
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
                return Unauthorized(AddLog(ex));
            }
            catch(Exception ex)
            {
                return BadRequest(AddLog(ex));
            }
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult?> GetTeamDetails([FromHeader] string idToken, [FromRoute] int teamId)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logicTeam = new TeamLogic(Context);
                var teamsData = logicTeam.GetTeamDetails(user.Email, teamId);
                return Json(teamsData);
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

        [HttpGet("{teamId}")]
        public async Task<ActionResult?> GetTeamCode([FromHeader] string idToken, [FromRoute] int teamId)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                var teamLogic = new TeamLogic(Context);
                GetTeamCodeDTO teamCode = teamLogic.GetTeamCode(teamId);
                teamCode.ExpireDate = teamCode.ExpireDate.ToLocalTime();
                return Json(teamCode);
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

        [HttpPost]
        public async Task<ActionResult?> JoinTeam([FromHeader] string idToken, JoinTeamDTO codeTeam)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
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
                return Unauthorized(AddLog(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(AddLog(ex));
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
                return Unauthorized(AddLog(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(AddLog(ex));
            }
        }

        [HttpPost("{teamId}")]
        public async Task<ActionResult> LeaveTeam([FromHeader] string idToken, [FromRoute] int teamId)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var teamLogic = new TeamLogic(Context);
                bool sucessfulOperation = teamLogic.LeaveTeam(user.Email, teamId);
                if (sucessfulOperation)
                    return Ok("");
                else
                    return BadRequest("Cannot leave a team !");
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

        [HttpDelete]
        [Route("{teamId}/{userId}")]
        public async Task<ActionResult> RemoveMember([FromHeader] string idToken, [FromRoute] int teamId, [FromRoute] int userId)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var teamLogic = new TeamLogic(Context);
                bool sucessfulOperation = teamLogic.RemoveMember(user.Email, teamId, userId);
                if (sucessfulOperation)
                    return Ok("");
                else
                    return BadRequest("Cannot remove user!");
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

        [HttpPut("{teamId}")]
        public async Task<ActionResult> UpdateTeam([FromHeader] string idToken, [FromRoute] int teamId, UpdateTeamDTO updatedTeam)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var teamLogic = new TeamLogic(Context);
                bool sucessfulOperation = teamLogic.UpdateTeam(teamId, updatedTeam);
                if (sucessfulOperation)
                    return Ok("");
                else
                    return BadRequest("Cannot change user role!");
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

        [HttpPut("{teamId}/{userId}")]
        public async Task<ActionResult> ChangeMemberRole([FromHeader] string idToken,[FromRoute] int teamId, [FromRoute] int userId, UpdatedMemberRoleDTO updatedMember)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var teamLogic = new TeamLogic(Context);
                bool sucessfulOperation = teamLogic.ChangeMemberRole(teamId, userId, updatedMember);
                if (sucessfulOperation)
                    return Ok("");
                else
                    return BadRequest("Cannot change user role!");
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
  