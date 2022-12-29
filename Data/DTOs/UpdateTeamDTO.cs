using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class UpdateTeamDTO
    {
        public string NewTeamName { get; set; }
        public string? NewLocation { get; set; }
        public string? NewOrganizationName { get; set; }
    }
}
