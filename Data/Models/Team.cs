using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required, MaxLength(50)]
        public string TeamName { get; set; }

        [Required, MaxLength(30)]
        public int TeamType { get; set; }

        [Required, MaxLength(30)]
        public int SportType { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? OrganizationName { get; set; }

    }

}