using Data.Models;
using System;

namespace Core.Models
{
    public class Teams
    {
        public List<TeamTO>? teams { get; set; } = new List<TeamTO>();

        public Teams(List<Team> _teams)
        {
            foreach (var team in _teams)
            {
                teams.Add(new TeamTO()
                {
                    Email = team.Email,
                    TeamName = team.TeamName,
                    TeamType = team.TeamType.ToString(),
                    SportType = team.SportType,
                    Location = team.Location,
                    OrganizationName = team.OrganizationName,
                });
            }
        }
    }

    public class TeamTO
    {
        public string? Email { get; set; }

        public string? TeamName { get; set; }

        public string? TeamType { get; set; }

        public string? SportType { get; set; }

        public string? Location { get; set; }

        public string? OrganizationName { get; set; }

        public Team GetNewTeam()
        {
            return new Team()
            {
                TeamId = Guid.NewGuid(),
                Email = this.Email,
                TeamName = this.TeamName,
                TeamType = (TeamType)Enum.Parse(typeof(TeamType), this.TeamType ?? "Amateur"),
                SportType = this.SportType,
                Location = this.Location,
                OrganizationName = this.OrganizationName
            };
        }
    }
}
