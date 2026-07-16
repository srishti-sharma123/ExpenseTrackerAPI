using ExpenseTrackerAPI.Models.Domain;
namespace ExpenseTrackerAPI.Repositories.Interfaces;
public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();

    Task<Transaction?> GetByIdAsync(int id);

    Task<Transaction> CreateAsync(Transaction transaction);

    Task<Transaction?> UpdateAsync(Transaction transaction);

    Task<Transaction?> DeleteAsync(int id);
}