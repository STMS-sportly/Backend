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
        }

        public IEnumerable<Team> GetUserTeams(string email)
        {
            var userTeamsId = teamContext.UsersTeams?.Where(e => e.Email == email).ToList();
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

        public void Save()
        {
            teamContext.SaveChanges();
        }

        public void UpdateTeam(Team team)
        {
            teamContext.Entry(team).State = EntityState.Modified;
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
