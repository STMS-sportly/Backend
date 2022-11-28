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
                    Role = Enum.GetName(typeof(EUserType), id.UserType),
                    MembersCount = teamRepo.GetNumberOfTeamMembers(id.TeamId)
                });
            }

            return res;
        }

        public GetTeamDetailsDTO GetTeamDetails(string email, int teamId)
        {
            var team = teamRepo.GetUserTeamById(teamId);
            var userTeam = teamRepo.GetUserTeam(email, teamId);
            if (userTeam.UserId == -1)
            {
                return new GetTeamDetailsDTO();
            }

            var members = teamRepo.GetMembers(teamId);
            List<Member> finalMembers = new List<Member>();
            foreach(var member  in members)
            {
                var memberStatus = teamRepo.GetUserTeam(member.Email, teamId);
                finalMembers.Add(
                    new Member()
                    {
                        Id = member.UserId,
                        FirstName = member.Firstname,
                        LastName = member.Surname,
                        IsAdmin = teamRepo.IsAdmin(memberStatus.UserType)
                    });
            }

            var res = new GetTeamDetailsDTO()
            {
                Id = team.TeamId,
                Name = team.TeamName,
                Discipline = new GetDesciplinesDTO() { Name = Enum.GetName(typeof(EDiscipline), team.SportType)},
                IsAdmin = teamRepo.IsAdmin(userTeam.UserType),
                MembersCount = teamRepo.GetNumberOfTeamMembers(team.TeamId),
                Location = team.Location,
                OrganizationName = team.OrganizationName,
                JoinedDate = userTeam.JoinedDate,
                Members = finalMembers.ToArray()
            };

            return res;
        }

        public void Save()
        {
            teamRepo.Save();
        }

        public string GetTeamCode(int teamId)
        {
            var code = teamRepo.GetTeamCode(teamId);
            return code.Code;
        }
    }
}
