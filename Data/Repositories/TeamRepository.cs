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
            teamContext.UsersTeams?.Add(new UserTeam { UserId = userId, TeamId = team.TeamId, UserType = team.TeamType });
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
                        Discipline = new { Name = (EDiscipline)tmp.SportType },
                        Type = (ETeamType)tmp.TeamType,
                        MembersCount = teamMembers
                    }).ToList<object>();
        }

        public IEnumerable<Team> GetTeamDetails(string email)
        {
            var userId = teamContext.Users.Where(e => e.Email == email).Select(e => e.UserId).FirstOrDefault();
            var userTeamsId = teamContext.UsersTeams?.Where(e => e.UserId == userId).ToList();
            var userTeams = new List<Team>();
            foreach (var tmp in userTeamsId ?? Enumerable.Empty<UserTeam>())
            {
                var team = teamContext.Teams?.Where(e => e.TeamId == tmp.TeamId).FirstOrDefault();
                if (team != null)
                {
                    userTeams.Add(team);
                }
            }

            return userTeams;
        }

        public IEnumerable<Team> GetTeamDetails(int teamId)
        {
            throw new NotImplementedException();
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
