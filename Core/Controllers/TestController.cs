using Core.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
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
    public class TestController : ControllerBase
    {

        [HttpPost]
        public async Task ListAllUsers()
        {
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
            while (await responses.MoveNextAsync())
            {
                ExportedUserRecords response = responses.Current;
                foreach (ExportedUserRecord user in response.Users)
                {
                    Console.WriteLine($"User: {user.Uid}");
                }
            }

            // Iterate through all users. This will still retrieve users in batches,
            // buffering no more than 1000 users in memory at a time.
            var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
            //while (await enumerator.MoveNextAsync())
            //{
            //    ExportedUserRecord user = enumerator.Current;
            //    Console.WriteLine($"User: {user.Uid}, Email:{user.Email}, {user.DisplayName}, {user.PhotoUrl}");
            //}

        }
    }
}
