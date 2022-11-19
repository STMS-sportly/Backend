using Data.Models;
using Logic.TeamLogic.Disciplines;

namespace Core.DTOs
{
    public class GetTeamsDTO
    {
        public Guid Id { get; set; }

        public string TeamName { get; set; }

        public DisciplineName Discipline { get; set; }

        public bool IsAdmin { get; set; }

        public int MembersCount { get; set; }
    }
}
