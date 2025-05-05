using System.ComponentModel.DataAnnotations;

namespace Ubereats.DTO
{
    public record UserDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string Name { get; init; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; init; }
        [Phone]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; init; }
        [Required(ErrorMessage = "Please enter your password")]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "Password and confirm password does not match")]
        public string Password { get; init; }
        [Required(ErrorMessage = "Please confirm your password")]
        public string ConfirmPassword { get; init; }
        // [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.")]
        // public decimal Price { get; init; }
    }
}