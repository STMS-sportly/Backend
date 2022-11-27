using Data.Enums;
using Data.Models;
using Logic.TeamLogic.Disciplines;

namespace Core.DTOs
{
    public class CreateTeamDTO
    {
        public string TeamName { get; set; }

        public Discipline Discipline { get; set; }

        public string TeamType { get; set; }

        public string? Location { get; set; }

        public string? OrganizationName { get; set; }

        public static implicit operator Team(CreateTeamDTO dto)
        {
            return new Team()
            {
                TeamName = dto.TeamName,
                TeamType = (int)(ETeamType)Enum.Parse(typeof(ETeamType),dto.TeamType),
                SportType = (int)(EDiscipline)Enum.Parse(typeof(EDiscipline), dto.Discipline.Name),
                Location = dto.Location,
                OrganizationName = dto.OrganizationName
            };
        }
    }

    public struct Discipline
    {
        public string Name { get; set; }
    }
}
