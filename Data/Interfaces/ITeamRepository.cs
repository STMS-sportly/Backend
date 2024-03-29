﻿using Data.Models;
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
        bool UpdateTeam(Team team);
        void Save();
        Team GetUserTeamById(int teamId);
        int GetNumberOfTeamMembers(int teamId);
        UserTeam GetUserTeam(string email, int teamId);
        List<User> GetMembers(int teamId);
        TeamCode GetTeamCode(int teamId);
        bool JoinTeam(string email, string codeTeam);
        void DeleteTeam(int teamId);
        bool LeaveTeam(string email, int teamId);
        bool RemoveMember(string email, int teamId, int teamMemberId);
        bool ChangeMemberRole(UserTeam newUserTeam);
    }
}
