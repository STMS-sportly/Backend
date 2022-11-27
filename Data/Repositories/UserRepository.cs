using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StmsContext userContext;

        public UserRepository(StmsContext userContext)
        {
            this.userContext = userContext;
        }

        public User GetUserByEmail(string email)
        {
            return userContext.Users.Where(e => e.Email == email).FirstOrDefault() ?? new User();
        }

        public List<UserTeam> GetUsersTeams(string email)
        {
            int userId = userContext.Users.Where(e => e.Email == email).Select(e => e.UserId).FirstOrDefault();

            return userContext.UsersTeams.Where(e => e.UserId == userId).Select(e => new UserTeam(){ TeamId = e.TeamId, UserType = e.UserType }).ToList() ?? new List<UserTeam>();
        }

        public bool UserExists(string email)
        {
            var user = userContext.Users.Where(e => e.Email == email).FirstOrDefault();
            return user != null;
        }

        public void InsertUser(User user)
        {
            userContext.Users.Add(user);
        }

        public void DeleteUser(string email)
        {
            User? user = userContext.Users.Where(e =>e.Email == email).FirstOrDefault();
            if (user != null)
            {
                userContext.Users?.Remove(user);
            }
        }

        public void UpdateUser(User user)
        {
            userContext.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            userContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                userContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
