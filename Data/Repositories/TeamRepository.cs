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
            Console.WriteLine(team.TeamId);
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
                        Type =  Enum.GetName(typeof(ETeamType), tmp.TeamType),
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
                return new UserTeam() { TeamId = -1};
            }
            return teamContext.UsersTeams.Where(e => e.UserId == user.UserId).FirstOrDefault() ?? new UserTeam();
        }

        public List<User> GetMembers(int teamId)
        {
            var userTeam = teamContext.UsersTeams.Where(e => e.TeamId == teamId).ToList();
            var res = new List<User>();
            foreach(var user in userTeam)
            {
                var newUser = teamContext.Users.Where(e => e.UserId == user.UserId).FirstOrDefault() ?? new User() { UserId = -1};
                if (newUser.UserId != -1)
                {
                    res.Add(newUser);
                }
            }

            return res;
        }

        public void UpdateTeam(Team team)
        {
            teamContext.Entry(team).State = EntityState.Modified;
        }

        public void Save()
        {
            teamContext.SaveChanges();
        }

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

        public bool IsAdmin(int userType)
        {
            return (userType == 0 || userType == 1);
        }

        public TeamCode GetTeamCode(int teamId)
        {
            var code = teamContext.TeamCodes.Where(e => e.TeamId == teamId).FirstOrDefault();
            var codes = teamContext.TeamCodes.Select(e => e.Code).ToList();
            if (code == null || DateTime.Compare(code.ExpireDate, DateTime.UtcNow) < 0)
            {
                if (code != null)
                {
                    teamContext.TeamCodes.Remove(code);
                    Save();
                }

                bool codeExists;
                string newCode = "000000";
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
                    ExpireDate = DateTime.UtcNow.AddDays(2)
                };

                teamContext.TeamCodes.Update(code);
                Save();
            }

            return code;
        }
    }
}
