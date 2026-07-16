using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.DTOs.User
{
    public class CreateUserRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}