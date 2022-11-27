using Data.DataAccess;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using FirebaseAdmin.Auth;
using Logic.ALL.DTOs;

namespace Logic.BLL
{
    public class TeamLogic
    {
        private readonly ITeamRepository teamRepo;

        public TeamLogic(StmsContext context)
        {
            teamRepo = new TeamRepository(context);
        }

        public void CreateTeam(UserRecord user, CreateTeamDTO newTeam)
        {
            var team = new Team()
            {
                TeamName = newTeam.TeamName,
                TeamType = (int)(ETeamType)Enum.Parse(typeof(ETeamType), newTeam.TeamType),
                SportType = (int)(EDiscipline)Enum.Parse(typeof(EDiscipline), newTeam.Discipline.Name),
                Location = newTeam.Location,
                OrganizationName = newTeam.OrganizationName
            };
            teamRepo.InsertTeam(team, user.Email);
            Save();
        }

        public List<GetTeamsDTO> GetTeams(List<UserTeam> userTeamsId)
        {
            var res = new List<GetTeamsDTO>();
            foreach(var id in userTeamsId)
            {
                var team = teamRepo.GetUserTeamById(id.TeamId);
                res.Add(new GetTeamsDTO()
                {
                    Id = team.TeamId,
                    TeamName = team.TeamName,
                    Discipline = new GetDesciplinesDTO() { Name = Enum.GetName(typeof(EDiscipline), team.SportType) },
                    Type = Enum.GetName(typeof(ETeamType), team.TeamType),
                    Role = Enum.GetName(typeof(EAdminType), id.UserType),
                    MembersCount = teamRepo.GetNumberOfTeamMembers(id.TeamId)
                });
            }

            return res;
        }

        // TeamDetails
        public object GetTeamDetails(string email, int teamId)
        {
            return teamRepo.GetNumberOfTeamMembers(teamId);
        }

        public void Save()
        {
            teamRepo.Save();
        }


    }
}
