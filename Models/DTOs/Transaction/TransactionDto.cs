namespace ExpenseTrackerAPI.Models.DTOs.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaidTo { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}