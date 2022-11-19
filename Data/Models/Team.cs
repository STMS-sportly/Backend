using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Team
    {
        [Key]
        public Guid TeamId { get; set; }

        [ForeignKey("User"), DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required, MaxLength(50)]
        public string? TeamName { get; set; }

        [Required, MaxLength(30)]
        public TeamType TeamType { get; set; }

        [Required, MaxLength(30)]
        public string? SportType { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? OrganizationName { get; set; }
    }

    public enum TeamType
    {
        Amateur,
        Professional
    }

}
