using Data.DataAccess;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.SymbolStore;

namespace Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly StmsContext teamContext;

        public TeamRepository(StmsContext teamContext)
        {
            this.teamContext = teamContext;
        }

        public void InsertTeam(Team team, string email)
        {
            teamContext.Teams.Add(team);
            Save();
            var userId = teamContext.Users.Where(e => e.Email == email).Select(e => e.UserId).FirstOrDefault();
            teamContext.UsersTeams?.Add(new UserTeam { UserId = userId, TeamId = team.TeamId, UserType = team.TeamType, JoinedDate = DateTime.UtcNow });
        }

        public List<object> GetTeams(List<UserTeam> userTeamsId)
        {
            return (from userTeam in userTeamsId
                    let tmp = teamContext.Teams.Where(e => e.TeamId == userTeam.TeamId).FirstOrDefault()
                    let teamMembers = teamContext.UsersTeams.Where(e => e.TeamId == userTeam.TeamId).Count()
                    where tmp != null
                    select new
                    {
                        TeamId = tmp.TeamId,
                        TeamName = tmp.TeamName,
                        Discipline = new { Name = Enum.GetName(typeof(EDiscipline), tmp.SportType) },
                        Type = Enum.GetName(typeof(ETeamType), tmp.TeamType),
                        MembersCount = teamMembers
                    }).ToList<object>();
        }

        public Team GetUserTeamById(int teamId)
        {
            return teamContext.Teams.Where(e => e.TeamId == teamId).FirstOrDefault() ?? new Team();
        }

        public int GetNumberOfTeamMembers(int teamId)
        {
            return teamContext.UsersTeams.Where(e => e.TeamId == teamId).Count();
        }

        public UserTeam GetUserTeam(string email, int teamId)
        {
            var user = teamContext.Users.Where(e => e.Email == email).FirstOrDefault();
            if (user == null)
            {
                return new UserTeam() { TeamId = -1 };
            }
            return teamContext.UsersTeams.Where(e => e.UserId == user.UserId && e.TeamId == teamId).FirstOrDefault() ?? new UserTeam();
        }

        public List<User> GetMembers(int teamId)
        {
            var userTeam = teamContext.UsersTeams.Where(e => e.TeamId == teamId).ToList();
            var res = new List<User>();
            foreach (var user in userTeam)
            {
                var newUser = teamContext.Users.Where(e => e.UserId == user.UserId).FirstOrDefault() ?? new User() { UserId = -1 };
                if (newUser.UserId != -1)
                {
                    res.Add(newUser);
                }
            }

            return res;
        }

        public bool UpdateTeam(Team team)
        {
            var oldteam = teamContext.Teams.Where(e => e.TeamId == team.TeamId).FirstOrDefault();
            if (oldteam != null)
            {
                oldteam.TeamName = team.TeamName;
                oldteam.Location = team.Location;
                oldteam.OrganizationName = team.OrganizationName;
                teamContext.SaveChanges();
                return true;
            }

            return false;
        }

        public TeamCode GetTeamCode(int teamId)
        {
            var code = teamContext.TeamCodes.Where(e => e.TeamId == teamId).FirstOrDefault();
            var codes = teamContext.TeamCodes.Select(e => e.Code).ToList();
            if (code != null)
            {
                var t1 = code.ExpireDate.ToUniversalTime();
                var t2 = DateTime.UtcNow.AddMinutes(15).ToUniversalTime();
                var codeTimeIsValid = DateTime.Compare(code.ExpireDate.ToUniversalTime(), DateTime.UtcNow.AddMinutes(15).ToUniversalTime());
                if (codeTimeIsValid > 0)
                {
                    return code;
                }
                teamContext.TeamCodes.Remove(code);
                Save();
            }

            bool codeExists;
            string newCode;
            do
            {
                codeExists = false;
                Random r = new Random();
                newCode = (r.Next(100_000, 999_999)).ToString();

                if (codes.Contains(newCode))
                {
                    codeExists = true;
                }

            } while (codeExists);

            code = new TeamCode()
            {
                Code = newCode,
                TeamId = teamId,
                ExpireDate = DateTime.UtcNow.AddHours(1).ToLocalTime()
            };

            teamContext.TeamCodes.Add(code);
            Save();

            return code;
        }

        public bool JoinTeam(string email, string codeTeam)
        {
            TeamCode code;
            try
            {
                code = teamContext.TeamCodes.Where(e => e.Code == codeTeam).First();
            }
            catch
            {
                return false;
            }
            var user = teamContext.Users.Where(e => e.Email == email).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var teamType = teamContext.Teams.Where(e => e.TeamId == code.TeamId).Select(e => e.TeamType).FirstOrDefault();

            var checkIfUserIsInTeam = teamContext.UsersTeams.Where(e => e.TeamId == code.TeamId && e.UserId == user.UserId).FirstOrDefault();

            if (checkIfUserIsInTeam == null)
            {
                teamContext.UsersTeams.Add(new UserTeam()
                {
                    UserId = user.UserId,
                    TeamId = code.TeamId,
                    UserType = teamType == 0 ? 2 : 3,
                    JoinedDate = DateTime.UtcNow
                });
            }
            Save();
            return true;
        }

        public void DeleteTeam(int teamId)
        {
            var teamCode = teamContext.TeamCodes.Where(e => e.TeamId == teamId).FirstOrDefault();
            if (teamCode != null)
            {
                teamContext.TeamCodes.Remove(teamCode);
            }
            Save();
            var userTeamsToDelete = teamContext.UsersTeams.Where(e => e.TeamId == teamId).ToArray();
            teamContext.UsersTeams.RemoveRange(userTeamsToDelete);
            Save();
            var team = teamContext.Teams.Where(e => e.TeamId == teamId).FirstOrDefault();
            if (team != null)
            {
                teamContext.Teams.Remove(team);
            }
            Save();

        }

        public bool LeaveTeam(string email, int teamId)
        {

            var user = teamContext.UsersTeams.Where(e => e.User.Email == email && e.TeamId == teamId).FirstOrDefault();
            if (user != null)
            {
                if (user.UserType != 0 && user.UserType != 1) // Not admin always can leave a team
                {
                    teamContext.UsersTeams.Remove(user);
                    Save();
                    return true;
                }
                else if (user.UserType == 1) // ProAdmin
                {
                    bool theOtherAdminExists = teamContext.UsersTeams.Where(e => e.TeamId == teamId && e.UserType == 1 && e.UserId != user.UserId).FirstOrDefault() != null;
                    if (theOtherAdminExists)
                    {
                        teamContext.UsersTeams.Remove(user);
                        Save();
                        return true;
                    }
                    else
                    {
                        var assistantExists = teamContext.UsersTeams.Where(e => e.TeamId == teamId && e.UserType == 4 && e.UserId != user.UserId).FirstOrDefault(); // Assistant
                        if (assistantExists != null)
                        {
                            assistantExists.UserType = 1;
                            teamContext.UsersTeams.Remove(user);
                            teamContext.UsersTeams.Update(assistantExists);
                            Save();
                            return true;
                        }
                        else
                        {
                            var newAdmin = teamContext.UsersTeams.Where(e => e.TeamId == teamId && e.UserId != user.UserId).FirstOrDefault();
                            if (newAdmin != null)
                            {
                                newAdmin.UserType = 1;
                                teamContext.UsersTeams.Remove(user);
                                teamContext.UsersTeams.Update(newAdmin);
                                Save();
                                return true;
                            }
                        }
                    }
                }
                else if (user.UserType == 0) // Admin
                {
                    bool theOtherAdminExists = teamContext.UsersTeams.Where(e => e.TeamId == teamId && e.UserType == 0 && e.UserId != user.UserId).FirstOrDefault() != null;
                    if (theOtherAdminExists)
                    {
                        teamContext.UsersTeams.Remove(user);
                        Save();
                        return true;
                    }
                    else
                    {
                        var newAdmin = teamContext.UsersTeams.Where(e => e.TeamId == teamId && e.UserId != user.UserId).FirstOrDefault();
                        if (newAdmin != null)
                        {
                            newAdmin.UserType = 0;
                            teamContext.UsersTeams.Remove(user);
                            teamContext.UsersTeams.Update(newAdmin);
                            Save();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool RemoveMember(string email, int teamId, int teamMemberId)
        {
            var user = teamContext.UsersTeams.Where(e => e.User.Email == email && e.TeamId == teamId).FirstOrDefault();

            if (user != null)
            {
                if (user.UserType != 0 && user.UserType != 1)
                    return false;

                var memberToDelete = teamContext.UsersTeams.Where(e => e.User.UserId == teamMemberId && e.TeamId == teamId).FirstOrDefault();

                if (memberToDelete != null)
                {
                    teamContext.UsersTeams.Remove(memberToDelete);
                    Save();
                    return true;
                }
            }

            return false;
        }

        public bool ChangeMemberRole(UserTeam newUserTeam)
        {
            var user = teamContext.UsersTeams.Where(e => e.UserId == newUserTeam.UserId && e.TeamId == newUserTeam.TeamId).FirstOrDefault();
            if (user != null)
            {
                user.UserType = newUserTeam.UserType;
                Save();
                return true;
            }

            return false;
        }

        public void Save()
        {
            teamContext.SaveChanges();
        }

        #region Dispose 

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                teamContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
