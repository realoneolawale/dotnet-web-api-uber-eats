using System.ComponentModel.DataAnnotations;

namespace Ubereats.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Otp Otp { get; set; }
    }
}