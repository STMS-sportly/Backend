using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Session
{
    public  class FireabseSession
    {
        public FireabseSession(string token)
        {
            Token = token;  
        }

        public string Token { get; set; }

        public async Task<bool> SessionVerify()
        {
            try
            {
                await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(Token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CookieSession> CreateSession()
        {
            var options = new SessionCookieOptions()
            {
                ExpiresIn = TimeSpan.FromHours(12)
            };

            var sessionCookie = await FirebaseAuth.DefaultInstance.CreateSessionCookieAsync(Token, options);
            var cookieOptions = new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.Add(options.ExpiresIn),
                HttpOnly = true,
                Secure = true,
            };
            // this.Response.Cookies.Append("session", sessionCookie, cookieOptions)
            return new CookieSession(sessionCookie, cookieOptions);
        }
    }

    public class CookieSession
    {
        public CookieSession(string value, CookieOptions options)
        {
            CookieValue = value;
            CookieOptions = options;
        }

        public string CookieValue { get; set; }

        public CookieOptions CookieOptions { get; set; }
    }
}
