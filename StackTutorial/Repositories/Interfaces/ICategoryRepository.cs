using StackTutorial.Models;

namespace StackTutorial.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetByIdAsync(long id);
        Task<CategoryModel> AddAsync(CategoryModel category);
        Task<CategoryModel> UpdateAsync(CategoryModel category);
        Task DeleteAsync(long id);
    }
}