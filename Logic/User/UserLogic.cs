using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Data.Models;

namespace Logic.User
{
    public class UserLogic
    {
        private readonly IUserRepository userRepo;
        public UserLogic(STMSContext context)
        {
            userRepo = new UserRepository(context);
        }

        public void AddUser(UserRecord user)
        {
            var name = user.DisplayName.Split(' ');
            var newUser = new Model.User()
            {
                Email = user.Email,
                Firstname = name[0],
                Surname = name[1],
                PhoneNumber = user.PhoneNumber,
                PhotoUrl = user.PhotoUrl
            };

            userRepo.InsertUser(newUser);
        }

        public void Save()
        {
            userRepo.Save();
        }

        public IEnumerable<Team> GetUserTeams(UserRecord user)
        {
            var team = userRepo.GetUserTeams(user.Email);
            return team; 
        }

        public bool UserExist(UserRecord user)
        {
            return userRepo.GetUserByEmail(user.Email) != null;
        }
    }
}
