using StackTutorial.Models;
using static StackTutorial.Models.CommonClass;

namespace StackTutorial.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentModel>> GetAllAsync();
        Task<IEnumerable<ContentModel>> GetByTopicIdAsync(long topicId);
        Task<ContentModel> GetByIdAsync(long id);
        Task<ContentModel> AddAsync(ContentModel content);
        Task<ContentModel> UpdateAsync(ContentModel content);
        Task DeleteAsync(long id);
        Task<ContentModel> GetPreviousContentAsync(long currentContentId, long topicId);
        Task<ContentModel> GetNextContentAsync(long currentContentId, long topicId);
        Task<List<CommonModel>> CountContentCategoriesWise();
    }
}