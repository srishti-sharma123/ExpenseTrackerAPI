using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.DTOs.User
{
    public class UpdateUserRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}