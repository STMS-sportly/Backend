using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITeamRepository : IDisposable
    {
        void InsertTeam(Team team);
        void UpdateTeam(Team team);
        void Save();
        IEnumerable<Team> GetTeamDetails(int teamId);
        List<Object> GetTeams(List<UserTeam> userTeamsId);
    }
}
