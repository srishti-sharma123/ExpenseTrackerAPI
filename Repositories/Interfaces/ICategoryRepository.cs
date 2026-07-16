using ExpenseTrackerAPI.Models.Domain;
namespace ExpenseTrackerAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteAsync(int id);
    }
}
