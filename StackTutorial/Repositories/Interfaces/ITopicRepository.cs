using StackTutorial.Models;

namespace StackTutorial.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<IEnumerable<TopicModel>> GetAllAsync();
        Task<IEnumerable<TopicModel>> GetByCategoryIdAsync(long categoryId);
        Task<TopicModel> GetByIdAsync(long id);
        Task<TopicModel> AddAsync(TopicModel topic);
        Task<TopicModel> UpdateAsync(TopicModel topic);
        Task DeleteAsync(long id);
    }
}