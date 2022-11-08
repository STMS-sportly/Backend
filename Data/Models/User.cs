using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        [Required]
        [MaxLength(254)]
        public string? Email { get; set; }

        public string? Firstname { get; set; }

        public string? Surname { get; set; }

        public string? PhoneNumber { get; set; }

        public string? PhotoUrl { get; set; }

        public Guid UserId { get; set; }

    }

}
