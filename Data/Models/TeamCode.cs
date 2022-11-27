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

        [Required]
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }

    }
}
