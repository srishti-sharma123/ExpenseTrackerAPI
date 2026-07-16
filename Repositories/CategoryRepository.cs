using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models.Domain;
using ExpenseTrackerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public CategoryRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) 
            {
                return null;
            }
            _context .Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;    
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}