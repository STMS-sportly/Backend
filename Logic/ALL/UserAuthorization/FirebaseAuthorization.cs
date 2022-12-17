using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ALL.UserAuthorization
{
    public static class FirebaseAuthorization
    {
        public static async Task<UserRecord> FirebaseUser(string idToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string userid = decodedToken.Uid;
            return await FirebaseAuth.DefaultInstance.GetUserAsync(userid);
        }
    }
}
