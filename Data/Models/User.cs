using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required, MaxLength(64)]
        public string? Firstname { get; set; }

        [Required, MaxLength(64)]
        public string? Surname { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [MaxLength(128)]
        public string? PhotoUrl { get; set; }

    }

}
