using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string email);
        void InsertUser(User user);
        void DeleteUser(string email);
        void UpdateUser(User user);
        void Save();
        IEnumerable<Team> GetUserTeams(string email);
    }
}
