using Microsoft.EntityFrameworkCore;
using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Data;

namespace StackTutorial.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public CategoryRepository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _DbContext.Categories.ToListAsync();
        }

        public async Task<CategoryModel> GetByIdAsync(long id)
        {
            return await _DbContext.Categories.FindAsync(id);
        }

        public async Task<CategoryModel> AddAsync(CategoryModel category)
        {
            _DbContext.Categories.Add(category);
            await _DbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel category)
        {
            _DbContext.Entry(category).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(long id)
        {
            var category = await _DbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _DbContext.Categories.Remove(category);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}