using System.ComponentModel.DataAnnotations;

namespace Ubereats.Helpers
{
    public record LoginDto
    {
        [Required(ErrorMessage = "Please provide your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide your password")]
        public string Password { get; set; }
    }

    public record LoginResponseDto(string Token, string TokenType, int UserId, string Email, string Name);
}