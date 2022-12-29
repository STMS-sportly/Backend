using Logic.ALL.UserAuthorization;
using Logic.BLL;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Auth;
using Data.DTOs;

namespace Core.Controllers
{
    [ApiController]
    [Route("chat/[action]")]
    public class ChatController : BaseController
    {
        [HttpPost("{teamId}")]
        public async Task<ActionResult> SendMessage([FromHeader] string idToken, [FromRoute]int teamId, string message)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ChatLogic(Context);
                var succesfullOperation = await logic.SendMessage(user.Email, teamId, message);
                if (succesfullOperation)
                    return Ok();
                else
                    return BadRequest("Problem with sending message");
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
        public async Task<GetChatMessagesDTO> GetMessages([FromHeader] string idToken, [FromRoute]int teamId)
        {
            try
            {
                var user = await FirebaseAuthorization.FirebaseUser(idToken);
                var logic = new ChatLogic(Context);
                var listOfMessages = logic.GetMessages(user.Email, teamId);
                return listOfMessages.Result;
            }
            catch
            {
                return new GetChatMessagesDTO();
            }
        }

        #region Possible extension
        //[HttpGet("{teamId}")]
        //public async Task<GetChatMessagesDTO> GetNextMessages([FromHeader] string idToken, [FromRoute] int teamId)
        //{
        //    return new GetChatMessagesDTO();
        //}
        #endregion
    }
}
