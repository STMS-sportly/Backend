using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class GetDayEventsDTO
    {
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Editable { get; set; } = true;
    }
}
