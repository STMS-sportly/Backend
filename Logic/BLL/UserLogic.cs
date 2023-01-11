using Data.DataAccess;
using Data.DTOs;
using Data.Interfaces;
using Data.Models;
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
            var newUser = new User()
            {
                Email = user.Email,
                Firstname = name[0] ?? "NoName",
                Surname = name[1] ?? "NoSurname",
                PhoneNumber = user.PhoneNumber,
                PhotoUrl = user.PhotoUrl
            };

            userRepo.InsertUser(newUser);
            Save();
        }

        public void Save()
        {
            userRepo.Save();
        }

        public List<UserTeam> GetUsersTeams(string email)
        {
            return userRepo.GetUsersTeams(email) ?? new List<UserTeam>();
        }

        public bool UserExist(UserRecord user)
        {
            return userRepo.UserExists(user.Email);
        }

        public GetUserDataDTO GetUserData(string email)
        {
            var userData = userRepo.GetUserByEmail(email);
            var result = new GetUserDataDTO()
            {
                FirstName = userData.Firstname,
                LastName = userData.Surname,
                UserId = userData.UserId,
            };
            return result;
        }
    }
}   
