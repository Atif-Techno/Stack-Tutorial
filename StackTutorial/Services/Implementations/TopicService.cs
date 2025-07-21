using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Services.Interfaces;

namespace StackTutorial.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IEnumerable<TopicModel>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TopicModel>> GetTopicsByCategoryIdAsync(long categoryId)
        {
            return await _topicRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<TopicModel> GetTopicByIdAsync(long id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }

        public async Task<TopicModel> CreateTopicAsync(TopicModel topic)
        {
            return await _topicRepository.AddAsync(topic);
        }

        public async Task<TopicModel> UpdateTopicAsync(TopicModel topic)
        {
            return await _topicRepository.UpdateAsync(topic);
        }

        public async Task DeleteTopicAsync(long id)
        {
            await _topicRepository.DeleteAsync(id);
        }
    }
}