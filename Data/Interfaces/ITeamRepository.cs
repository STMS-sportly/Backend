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
        void InsertTeam(Team team, string email);
        void UpdateTeam(Team team);
        void Save();
        Team GetUserTeamById(int teamId);
        int GetNumberOfTeamMembers(int teamId);
        UserTeam GetUserTeam(string email, int teamId);
        bool IsAdmin(int userType);
        List<User> GetMembers(int teamId);
    }
}
