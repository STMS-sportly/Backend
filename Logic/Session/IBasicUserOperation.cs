using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Session
{
    public interface IBasicUserOperation
    {
        void Login();
        void Logout();
        Task<string?> VerifySessinAsync(string _idToken);
    }
}
