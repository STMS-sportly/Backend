using FirebaseAdmin.Auth;
using Logic.ALL.DTOs;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [ApiController]
    [Route("schedule/[action]")]
    public class ScheduleController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateEvent([FromHeader] string idToken, CreateEventDTO newEvent)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logic = new ScheduleLogic(Context);
                bool succesfullOperation = logic.CreateEvent(user.Email, newEvent);
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

        [HttpGet]
        public async Task<ActionResult> GetMonthEvents([FromHeader] string idToken, [FromRoute] int teamId, DateTime date)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logic = new ScheduleLogic(Context);
                var events = logic.GetMonthEvents(teamId, date);
                return Json(new {Events = events});
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
        public async Task<ActionResult> GetDayEvents([FromHeader] string idToken, [FromRoute] int teamId, DateTime date)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string userid = decodedToken.Uid;
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
                var logic = new ScheduleLogic(Context);
                var events = logic.GetDayEvents(user.Email, teamId, date);
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
