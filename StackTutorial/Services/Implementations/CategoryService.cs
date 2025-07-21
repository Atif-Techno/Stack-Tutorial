using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Services.Interfaces;

namespace StackTutorial.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(long id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(long id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}