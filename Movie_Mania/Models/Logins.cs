using System.ComponentModel.DataAnnotations;

namespace Movie_Mania.Models
{
    public class Logins
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class LoginMemo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}