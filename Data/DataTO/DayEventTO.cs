﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataTO
{
    public class DayEventTO
    {
        public int EventId { get; set; }
        public bool Editable { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
