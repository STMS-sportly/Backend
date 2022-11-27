namespace Core.DTOs
{
    public class GetTeamDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DisciplineDto Discipline { get; set; }
        public bool IsAdmin { get; set; }
        public int MembersCount { get; set; }
        public string? Location { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime JoinedDate { get; set; }
        public Member[] Members { get; set; }
    }

    public class DisciplineDto
    {
        public dynamic Name { get; set; }
    }

    public class Member
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
