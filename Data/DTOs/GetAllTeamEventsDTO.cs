using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class GetAllTeamEventsDTO
    {
        public List<GetDayEventsDTO> TeamEvents = new List<GetDayEventsDTO>();
    }
}
