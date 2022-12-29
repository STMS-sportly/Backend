using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ALL.DTOs
{
    public class GetMonthEventsDTO
    {
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}
