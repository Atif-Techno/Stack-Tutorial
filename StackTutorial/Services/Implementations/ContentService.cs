using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Services.Interfaces;
using static StackTutorial.Models.CommonClass;

namespace StackTutorial.Services.Implementations
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<IEnumerable<ContentModel>> GetAllContentsAsync()
        {
            return await _contentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ContentModel>> GetContentsByTopicIdAsync(long topicId)
        {
            return await _contentRepository.GetByTopicIdAsync(topicId);
        }

        public async Task<ContentModel> GetContentByIdAsync(long id)
        {
            return await _contentRepository.GetByIdAsync(id);
        }

        public async Task<ContentModel> CreateContentAsync(ContentModel content)
        {
            return await _contentRepository.AddAsync(content);
        }

        public async Task<ContentModel> UpdateContentAsync(ContentModel content)
        {
            return await _contentRepository.UpdateAsync(content);
        }

        public async Task DeleteContentAsync(long id)
        {
            await _contentRepository.DeleteAsync(id);
        }

        public async Task<ContentModel> GetPreviousContentAsync(long currentContentId, long topicId)
        {
            return await _contentRepository.GetPreviousContentAsync(currentContentId, topicId);
        }

        public async Task<ContentModel> GetNextContentAsync(long currentContentId, long topicId)
        {
            return await _contentRepository.GetNextContentAsync(currentContentId, topicId);
        }
        public async Task<List<CommonModel>> CountContentCategoriesWise()
        {
            return await _contentRepository.CountContentCategoriesWise();
        }
    }
}