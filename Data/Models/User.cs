using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(64)]
        public string Firstname { get; set; }

        [Required, MaxLength(64)]
        public string Surname { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [MaxLength(128)]
        public string? PhotoUrl { get; set; }

        public virtual List<UserTeam> UserTeams { get; set; }
    }

}
