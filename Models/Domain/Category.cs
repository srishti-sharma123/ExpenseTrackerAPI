namespace ExpenseTrackerAPI.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();  //since it has many transactions
    }
}
