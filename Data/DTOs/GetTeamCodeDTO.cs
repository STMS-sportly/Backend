using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class GetTeamCodeDTO
    {
        public string Code { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
