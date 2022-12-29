using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class GetTeamsDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public GetDesciplinesDTO Discipline { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public int MembersCount { get; set; }
    }
}
