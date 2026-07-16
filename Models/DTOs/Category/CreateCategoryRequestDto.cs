using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.DTOs.Category
{
    public class CreateCategoryRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}