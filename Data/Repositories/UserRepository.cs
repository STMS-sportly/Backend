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
        private readonly STMSContext userContext;

        public UserRepository(STMSContext userContext)
        {
            this.userContext = userContext;
        }

        public void DeleteUser(string email)
        {
            User user = userContext.Users.Find(email);
            if (user != null)
            {
                userContext.Users.Remove(user);
            }
        }

        public User GetUserByEmail(string? email)
        {
            return userContext.Users.Where(e => e.Email == email).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return userContext.Users.ToList();
        }

        public IEnumerable<Team> GetUserTeams(string email)
        {
            var userTeamsId = userContext.UsersTeams.Where(e => e.Email == email).ToList();
            var userTeams = new List<Team>();
            foreach(var tmp in userTeamsId)
            {
                var team = userContext.Teams.Where(e => e.TeamId == tmp.TeamId).FirstOrDefault();
                if (team != null)
                {
                    userTeams.Add(team);
                }
            }
            return userTeams;
        }

        public void InsertUser(User user)
        {
            userContext.Users.Add(user);
        }

        public void Save()
        {
            userContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            userContext.Entry(user).State = EntityState.Modified;
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
