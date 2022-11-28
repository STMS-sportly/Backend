namespace Logic.ALL.DTOs
{
    public class GetTeamDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetDesciplinesDTO Discipline { get; set; }
        public string teamType { get; set; }
        public string Role { get; set; }
        public int MembersCount { get; set; }
        public string? Location { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime JoinedDate { get; set; }
        public Member[] Members { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
