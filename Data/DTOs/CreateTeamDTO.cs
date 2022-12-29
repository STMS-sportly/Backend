namespace Data.DTOs
{
    public class CreateTeamDTO
    {
        public string TeamName { get; set; }

        public Discipline Discipline { get; set; }

        public string TeamType { get; set; }

        public string? Location { get; set; }

        public string? OrganizationName { get; set; }

    }

    public struct Discipline
    {
        public string Name { get; set; }
    }
}
