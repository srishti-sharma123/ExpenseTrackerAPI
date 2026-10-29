using ExpenseTrackerAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Repositories.Interfaces;

namespace ExpenseTrackerAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public TransactionRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        // for getting access for the authorise users and to see their own trasacrions 
        public async Task<IEnumerable<Transaction>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return null;
            }

            _context.Transactions.Remove(transaction);

            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> UpdateAsync(Transaction transaction)
        {
            var existingTransaction =
                await _context.Transactions.FindAsync(transaction.Id);

            if (existingTransaction == null)
            {
                return null;
            }

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.TransactionDate = transaction.TransactionDate;
            existingTransaction.Description = transaction.Description;
            existingTransaction.PaymentMethod = transaction.PaymentMethod;
            existingTransaction.PaidTo = transaction.PaidTo;
            existingTransaction.Type = transaction.Type;
            existingTransaction.UserId = transaction.UserId;
            existingTransaction.CategoryId = transaction.CategoryId;

            await _context.SaveChangesAsync();

            return existingTransaction;
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }
    }
}
