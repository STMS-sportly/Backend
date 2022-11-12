using Core.Models;
using Data.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Logic.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Core.Controllers
{

    [ApiController]
    [Route("api/[action]")]
    public class TestController : BaseController
    {

        [HttpGet]
        public async Task<JsonResult> ListAllUsers()
        {
            try
            {
                var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
                var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
                List<string> userId = new List<string>();
                while (await responses.MoveNextAsync())
                {
                    ExportedUserRecords response = responses.Current;
                    foreach (ExportedUserRecord user in response.Users)
                    {
                        Console.WriteLine($"User: {user.Uid}");
                        userId.Add(user.Uid);
                    }
                }
                return Json(userId);
            }
            catch(Exception ex)
            {
                var log = new LogsLogic(context);
                log.AddLog(ex.Message);
                return Json("ERROR");
            }
        }
    }
}
