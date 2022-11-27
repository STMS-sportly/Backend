using Data.DataAccess;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;

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

    }
}
