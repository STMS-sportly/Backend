using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }

        [ForeignKey("User")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TeamName { get; set; }
    }
}
