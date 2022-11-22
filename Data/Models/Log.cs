using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string? Message { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime LogTime { get; set; }
    }
}
