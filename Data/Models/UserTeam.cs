using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserTeam
    {
        public Guid TeamId { get; set; }
        public string? Email { get; set; }
    }
}
