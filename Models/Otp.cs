using System.ComponentModel.DataAnnotations;

namespace Ubereats.Models
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OTP { get; set; }
    }
}