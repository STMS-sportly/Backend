using Data.DataAccess;
using Data.Interfaces;
using Data.Repositories;
using FirebaseAdmin.Auth;
using Model = Data.Models;

namespace Logic.BLL
{
    public class UserLogic
    {
        private readonly IUserRepository userRepo;
        public UserLogic(StmsContext context)
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

        public bool UserExist(UserRecord user)
        {
            return userRepo.GetUserByEmail(user.Email) != null;
        }
    }
}
