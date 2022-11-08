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
        private UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext; 
        }

        public void DeleteUser(Guid id)
        {
            User user = userContext.Users.Find(id);
            userContext.Users.Remove(user);
        }

        public User GetUserById(Guid id)
        {
            return userContext.Users.Where(e => e.UserId == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return userContext.Users.ToList();
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
