using FirebaseAdmin.Auth;
using Logic.ALL.DTOs;
using Logic.ALL.UserAuthorization;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("schedule/[action]")]
    public class ScheduleController : BaseController
    {
        [HttpPost("{teamId}")]
        public async Task<ActionResult> CreateEvent([FromHeader] string idToken, [FromRoute] int teamId, CreateEventDTO newEvent)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ScheduleLogic(Context);
                bool succesfullOperation = logic.CreateEvent(user.Email, teamId, newEvent);
                if (succesfullOperation)
                    return Ok();
                else
                    return BadRequest("Problem with creating Event");
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
        public async Task<ActionResult> GetMonthEvents([FromHeader] string idToken, [FromRoute] int teamId, string date)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ScheduleLogic(Context);
                DateTime t = DateTime.Parse(date);
                var events = logic.GetMonthEvents(teamId, t);
                return Json(new { Events = events });
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
        public async Task<ActionResult> GetDayEvents([FromHeader] string idToken, [FromRoute] int teamId, string date)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ScheduleLogic(Context);
                DateTime t = DateTime.Parse(date);
                var events = logic.GetDayEvents(user.Email, teamId, t);
                return Json(new { Events = events });
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
