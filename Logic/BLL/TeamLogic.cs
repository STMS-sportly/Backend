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

        public void CreateTeam(Team newTeam)
        {
            teamRepo.InsertTeam(newTeam);
        }

        public List<object> GetTeams(List<UserTeam> userTeamsId)
        {
            return teamRepo.GetTeams(userTeamsId);
        }

        public void Save()
        {
            teamRepo.Save();
        }


    }
}
