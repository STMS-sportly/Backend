using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Team
    {
        [Key]
        public Guid TeamId { get; set; }

        [ForeignKey("User")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TeamName { get; set; }

        public bool IsProTeam { get; set; }

        public string SportType { get; set; }

        public string Location { get; set; }

        public string OrganizationName { get; set; }
    }
}
