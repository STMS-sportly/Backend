using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [ForeignKey("Team"), Required]
        public int TeamId { get; set; }

        [Required, MaxLength(100)]
        public string EventName { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime EventStart { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime EventEnd { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }
    }
}
