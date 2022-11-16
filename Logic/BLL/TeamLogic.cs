using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using FirebaseAdmin.Auth;

namespace Logic.BLL
{
    public class TeamLogic
    {
        private readonly ITeamRepository teamRepo;
        public TeamLogic(StmsContext context)
        {
            teamRepo = new TeamRepository(context);
        }

        public void CreateTeam(Team newTeam, UserRecord user)
        {
            throw new NotImplementedException();
        }

        public List<Team> GetUserTeams(UserRecord user)
        {
            return teamRepo.GetUserTeams(user.Email).ToList();
        }
    }
}
