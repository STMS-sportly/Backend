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

        [HttpDelete("{teamId}/{eventId}")]
        public async Task<ActionResult> RemoveEvent([FromHeader] string idToken, [FromRoute] int teamId, [FromRoute]int eventId)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ScheduleLogic(Context);
                bool success = logic.RemoveEvent(eventId, teamId);

                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Can not remove Event");
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

        [HttpPut("{teamId}/{eventId}")]
        public async Task<ActionResult> UpdateEvent([FromHeader] string idToken, [FromRoute] int teamId,[FromRoute] int eventId, UpdatedEventDTO updatedEvent)
        {
            if (updatedEvent.Date == default || updatedEvent.Title == null)
                throw new Exception("Date and Title are requeired !");

            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ScheduleLogic(Context);
                bool success = logic.UpdatedEvent(eventId, teamId, updatedEvent);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Can not update Event");
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
    }
}
