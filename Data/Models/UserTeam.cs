using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserTeam
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("Team"), Required]
        public int TeamId { get; set; }

        [Required]
        public int UserType { get; set; }
    }
}
