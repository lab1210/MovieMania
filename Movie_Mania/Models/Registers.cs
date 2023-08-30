using System.ComponentModel.DataAnnotations;

namespace Movie_Mania.Models
{
    public class Registers
    {
        public int ID { get; set; }
        [Required]

        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}