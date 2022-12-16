using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ALL.DTOs
{
    public class UpdatedMemberRoleDTO
    {
        public int TeamId { get; set; } 

        public int UserId { get; set; }

        public string NewRole { get; set; }
    }
}
