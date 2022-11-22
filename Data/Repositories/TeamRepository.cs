using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly StmsContext teamContext;

        public TeamRepository(StmsContext teamContext)
        {
            this.teamContext = teamContext;
        }

        public void InsertTeam(Team team)
        {
            teamContext.Teams?.Add(team);
            var userId = teamContext.Users.Where(e => e.Email == e.Email).Select(e => e.UserId).FirstOrDefault();
            teamContext.UsersTeams?.Add(new UserTeam { UserId = userId, TeamId = team.TeamId  });
        }

        public IEnumerable<Team> GetUserTeams(string email)
        {
            var userId = teamContext.Users.Where(e => e.Email == e.Email).Select(e => e.UserId).FirstOrDefault();
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
