﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ALL.DTOs
{
    public class UpdatedEventDTO
    {
        public DateTime eventDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
