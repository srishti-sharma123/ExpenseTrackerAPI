using ExpenseTrackerAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;
    }
}
