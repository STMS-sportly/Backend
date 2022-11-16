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
        [Required]
        public Guid TeamId { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
