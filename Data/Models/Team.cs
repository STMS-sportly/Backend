using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Team
    {

        public Guid TeamId { get; set; }

        public Guid TeamAdmin { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TeamName { get; set; }
    }
}
