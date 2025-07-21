using StackTutorial.Models;

namespace StackTutorial.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicModel>> GetAllTopicsAsync();
        Task<IEnumerable<TopicModel>> GetTopicsByCategoryIdAsync(long categoryId);
        Task<TopicModel> GetTopicByIdAsync(long id);
        Task<TopicModel> CreateTopicAsync(TopicModel topic);
        Task<TopicModel> UpdateTopicAsync(TopicModel topic);
        Task DeleteTopicAsync(long id);
    }
}