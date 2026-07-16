using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.DTOs.Transaction
{
    public class UpdateTransactionRequestDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        public string PaidTo { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}