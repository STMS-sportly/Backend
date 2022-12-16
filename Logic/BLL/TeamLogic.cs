using Data.DataAccess;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using FirebaseAdmin.Auth;
using Logic.ALL.DTOs;
using System.Diagnostics.Metrics;

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
                        Role = Enum.GetName(typeof(EUserType), memberStatus.UserType),
                    });
            }

            var user = teamRepo.GetUserTeam(email, teamId);
            var res = new GetTeamDetailsDTO()
            {
                Id = team.TeamId,
                Name = team.TeamName,
                Discipline = new GetDesciplinesDTO() { Name = Enum.GetName(typeof(EDiscipline), team.SportType)},
                teamType = Enum.GetName(typeof(ETeamType), team.TeamType),
                Role = Enum.GetName(typeof(EUserType), user.UserType),
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

        public GetTeamCodeDTO GetTeamCode(int teamId)
        {
            var code = teamRepo.GetTeamCode(teamId);
            return new GetTeamCodeDTO()
            {
                Code = code.Code,
                ExpireDate = code.ExpireDate
            };
        }

        public bool JoinTeam(string email, string codeTeam)
        {
            return teamRepo.JoinTeam(email, codeTeam);
        }

        public void DeleteTeam(int teamId)
        {
            teamRepo.DeleteTeam(teamId);
        }

        public bool LeaveTeam(string email, int teamId)
        {
            return teamRepo.LeaveTeam(email, teamId);
        }

        public bool RemoveMember(string email, int teamId, int teamMemberId)
        {
            return teamRepo.RemoveMember(email, teamId, teamMemberId);
        }

        public bool UpdateTeam(UpdateTeamDTO updatedTeam)
        {
            var newTeam = new Team()
            {
                TeamId = updatedTeam.TeamId,
                TeamName = updatedTeam.NewTeamName,
                Location = updatedTeam.NewLocation,
                OrganizationName = updatedTeam.NewOrganizationName
            };

            return teamRepo.UpdateTeam(newTeam);
        }

        public bool ChangeMemberRole(UpdatedMemberRoleDTO updatedMember)
        {
            var newUserTeam = new UserTeam()
            {
                UserId= updatedMember.UserId,
                TeamId= updatedMember.TeamId,
                UserType = (int)(Enum.Parse(typeof(EUserType), updatedMember.NewRole))
            };

            return teamRepo.ChangeMemberRole(newUserTeam);
        }
    }
}
