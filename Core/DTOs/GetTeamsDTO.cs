using Data.Models;
using Logic.TeamLogic.Disciplines;

namespace Core.DTOs
{
    public class GetTeamsDTO
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public DisciplineDto Discipline { get; set; }

        public string Type { get; set; }

        public string Role { get; set; }

        public int MembersCount { get; set; }
    }
}
