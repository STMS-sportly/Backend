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
        User GetUserById(Guid id);
        void InsertUser(User user);
        void DeleteUser(Guid id);
        void UpdateUser(User user);
        void Save();
    }
}
