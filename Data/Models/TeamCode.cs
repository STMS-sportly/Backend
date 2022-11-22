using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class TeamCode
    {
        [Key, StringLength(6)]
        public string? Code { get; set; }

        [ForeignKey("Team"), Required]
        public Guid TeamId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }

    }
}
